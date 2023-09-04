#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Rimaethon._Scripts.Utility
{
//This class is taken from C# library source code and modified to work with Unity 2020.3.1f1. 
    internal sealed class PriorityQueueDebugView<TElement, TPriority>
    {
        private readonly PriorityQueue<TElement, TPriority> _queue;
        private readonly bool _sort;

        public PriorityQueueDebugView(PriorityQueue<TElement, TPriority> queue)
        {
            ArgumentNullException.ThrowIfNull(queue);

            _queue = queue;
            _sort = true;
        }

        public PriorityQueueDebugView(PriorityQueue<TElement, TPriority>.UnorderedItemsCollection collection)
        {
            _queue = collection?._queue ?? throw new System.ArgumentNullException(nameof(collection));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public (TElement Element, TPriority Priority)[] Items
        {
            get
            {
                List<(TElement Element, TPriority Priority)> list = new(_queue.UnorderedItems);
                if (_sort) list.Sort((i1, i2) => _queue.Comparer.Compare(i1.Priority, i2.Priority));

                return list.ToArray();
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class SR
    {
        internal const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";
        internal const string ArgumentOutOfRange_IndexMustBeLessOrEqual = "Index must be less or equal";
        internal const string InvalidOperation_EmptyQueue = "The queue is empty.";
        internal const string InvalidOperation_EnumFailedVersion = "Collection modified while iterating over it.";
        internal const string Arg_NonZeroLowerBound = "Non-zero lower bound required.";
        internal const string Arg_RankMultiDimNotSupported = "Multi-dimensional arrays not supported.";
        internal const string Argument_InvalidArrayType = "Invalid array type.";
        internal const string Argument_InvalidOffLen = "Invalid offset or length.";
    }

    internal static class ArgumentNullException
    {
        public static void ThrowIfNull(object o)
        {
            if (o == null)
                throw new System.ArgumentNullException(); // hard to do it differently without C# 10's features
        }
    }

    internal static class ArrayEx
    {
        internal const int MaxLength = int.MaxValue;
    }


    internal static class EnumerableHelpers
    {
        internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
        {
            if (source is ICollection<T> ic)
            {
                var count = ic.Count;
                if (count != 0)
                {
                    // that implement ICollection<T>, which as of .NET 4.6 is just ConcurrentDictionary<TKey, TValue>.
                    var arr = new T[count];
                    ic.CopyTo(arr, 0);
                    length = count;
                    return arr;
                }
            }
            else
            {
                using (var en = source.GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        const int DefaultCapacity = 4;
                        var arr = new T[DefaultCapacity];
                        arr[0] = en.Current;
                        var count = 1;

                        while (en.MoveNext())
                        {
                            if (count == arr.Length)
                            {
                                var newLength = count << 1;
                                if ((uint)newLength > ArrayEx.MaxLength)
                                    newLength = ArrayEx.MaxLength <= count ? count + 1 : ArrayEx.MaxLength;

                                Array.Resize(ref arr, newLength);
                            }

                            arr[count++] = en.Current;
                        }

                        length = count;
                        return arr;
                    }
                }
            }

            length = 0;
            return Array.Empty<T>();
        }
    }


    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(PriorityQueueDebugView<,>))]
    public class PriorityQueue<TElement, TPriority>
    {
        private const int Arity = 4;


        private const int Log2Arity = 2;


        private readonly IComparer<TPriority>? _comparer;


        private (TElement Element, TPriority Priority)[] _nodes;

        private int _size;

        private UnorderedItemsCollection? _unorderedItems;


        private int _version;

#if DEBUG
        static PriorityQueue()
        {
            Debug.Assert(Log2Arity > 0 && Math.Pow(2, Log2Arity) == Arity);
        }
#endif

        public PriorityQueue()
        {
            _nodes = Array.Empty<(TElement, TPriority)>();
            _comparer = InitializeComparer(null);
        }

        public PriorityQueue(int initialCapacity)
            : this(initialCapacity, null)
        {
        }


        public PriorityQueue(IComparer<TPriority>? comparer)
        {
            _nodes = Array.Empty<(TElement, TPriority)>();
            _comparer = InitializeComparer(comparer);
        }


        public PriorityQueue(int initialCapacity, IComparer<TPriority>? comparer)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(initialCapacity), initialCapacity, SR.ArgumentOutOfRange_NeedNonNegNum);

            _nodes = new (TElement, TPriority)[initialCapacity];
            _comparer = InitializeComparer(comparer);
        }


        public PriorityQueue(IEnumerable<(TElement Element, TPriority Priority)> items)
            : this(items, null)
        {
        }


        public PriorityQueue(IEnumerable<(TElement Element, TPriority Priority)> items, IComparer<TPriority>? comparer)
        {
            ArgumentNullException.ThrowIfNull(items);

            _nodes = EnumerableHelpers.ToArray(items, out _size);
            _comparer = InitializeComparer(comparer);

            if (_size > 1) Heapify();
        }

        public int Count => _size;


        public IComparer<TPriority> Comparer => _comparer ?? Comparer<TPriority>.Default;


        public UnorderedItemsCollection UnorderedItems => _unorderedItems ??= new UnorderedItemsCollection(this);

        public void Enqueue(TElement element, TPriority priority)
        {
            var currentSize = _size++;
            _version++;

            if (_nodes.Length == currentSize) Grow(currentSize + 1);

            if (_comparer == null)
                MoveUpDefaultComparer((element, priority), currentSize);
            else
                MoveUpCustomComparer((element, priority), currentSize);
        }


        public TElement Peek()
        {
            if (_size == 0) throw new InvalidOperationException(SR.InvalidOperation_EmptyQueue);

            return _nodes[0].Element;
        }


        public TElement Dequeue()
        {
            if (_size == 0) throw new InvalidOperationException(SR.InvalidOperation_EmptyQueue);

            var element = _nodes[0].Element;
            RemoveRootNode();
            return element;
        }


        public bool TryDequeue([MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_size != 0)
            {
                (element, priority) = _nodes[0];
                RemoveRootNode();
                return true;
            }

            element = default;
            priority = default;
            return false;
        }

        public bool TryPeek([MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_size != 0)
            {
                (element, priority) = _nodes[0];
                return true;
            }

            element = default;
            priority = default;
            return false;
        }

        public TElement EnqueueDequeue(TElement element, TPriority priority)
        {
            if (_size != 0)
            {
                var root = _nodes[0];

                if (_comparer == null)
                {
                    if (Comparer<TPriority>.Default.Compare(priority, root.Priority) > 0)
                    {
                        MoveDownDefaultComparer((element, priority), 0);
                        _version++;
                        return root.Element;
                    }
                }
                else
                {
                    if (_comparer.Compare(priority, root.Priority) > 0)
                    {
                        MoveDownCustomComparer((element, priority), 0);
                        _version++;
                        return root.Element;
                    }
                }
            }

            return element;
        }

        public void EnqueueRange(IEnumerable<(TElement Element, TPriority Priority)> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var count = 0;
            var collection =
                items as ICollection<(TElement Element, TPriority Priority)>;
            if (collection is not null && (count = collection.Count) > _nodes.Length - _size) Grow(_size + count);

            if (_size == 0)
            {
                // build using Heapify() if the queue is empty.

                if (collection is not null)
                {
                    collection.CopyTo(_nodes, 0);
                    _size = count;
                }
                else
                {
                    var i = 0;
                    (TElement, TPriority)[] nodes = _nodes;
                    foreach ((var element, var priority) in items)
                    {
                        if (nodes.Length == i)
                        {
                            Grow(i + 1);
                            nodes = _nodes;
                        }

                        nodes[i++] = (element, priority);
                    }

                    _size = i;
                }

                _version++;

                if (_size > 1) Heapify();
            }
            else
            {
                foreach ((var element, var priority) in items) Enqueue(element, priority);
            }
        }

        public void EnqueueRange(IEnumerable<TElement> elements, TPriority priority)
        {
            ArgumentNullException.ThrowIfNull(elements);

            int count;
            if (elements is ICollection<(TElement Element, TPriority Priority)> collection &&
                (count = collection.Count) > _nodes.Length - _size)
                Grow(_size + count);

            if (_size == 0)
            {
                // build using Heapify() if the queue is empty.

                var i = 0;
                (TElement, TPriority)[] nodes = _nodes;
                foreach (var element in elements)
                {
                    if (nodes.Length == i)
                    {
                        Grow(i + 1);
                        nodes = _nodes;
                    }

                    nodes[i++] = (element, priority);
                }

                _size = i;
                _version++;

                if (i > 1) Heapify();
            }
            else
            {
                foreach (var element in elements) Enqueue(element, priority);
            }
        }

        public void Clear()
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<(TElement, TPriority)>())
                // Clear the elements so that the gc can reclaim the references
                Array.Clear(_nodes, 0, _size);

            _size = 0;
            _version++;
        }

        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, SR.ArgumentOutOfRange_NeedNonNegNum);

            if (_nodes.Length < capacity)
            {
                Grow(capacity);
                _version++;
            }

            return _nodes.Length;
        }

        public void TrimExcess()
        {
            var threshold = (int)(_nodes.Length * 0.9);
            if (_size < threshold)
            {
                Array.Resize(ref _nodes, _size);
                _version++;
            }
        }

        private void Grow(int minCapacity)
        {
            Debug.Assert(_nodes.Length < minCapacity);

            const int GrowFactor = 2;
            const int MinimumGrow = 4;

            var newcapacity = GrowFactor * _nodes.Length;

            if ((uint)newcapacity > ArrayEx.MaxLength) newcapacity = ArrayEx.MaxLength;

            // Ensure minimum growth is respected.
            newcapacity = Math.Max(newcapacity, _nodes.Length + MinimumGrow);

            if (newcapacity < minCapacity) newcapacity = minCapacity;

            Array.Resize(ref _nodes, newcapacity);
        }

        private void RemoveRootNode()
        {
            var lastNodeIndex = --_size;
            _version++;

            if (lastNodeIndex > 0)
            {
                var lastNode = _nodes[lastNodeIndex];
                if (_comparer == null)
                    MoveDownDefaultComparer(lastNode, 0);
                else
                    MoveDownCustomComparer(lastNode, 0);
            }

            if (RuntimeHelpers.IsReferenceOrContainsReferences<(TElement, TPriority)>())
                _nodes[lastNodeIndex] = default;
        }

        private static int GetParentIndex(int index)
        {
            return (index - 1) >> Log2Arity;
        }

        private static int GetFirstChildIndex(int index)
        {
            return (index << Log2Arity) + 1;
        }

        private void Heapify()
        {
            var nodes = _nodes;
            var lastParentWithChildren = GetParentIndex(_size - 1);

            if (_comparer == null)
                for (var index = lastParentWithChildren; index >= 0; --index)
                    MoveDownDefaultComparer(nodes[index], index);
            else
                for (var index = lastParentWithChildren; index >= 0; --index)
                    MoveDownCustomComparer(nodes[index], index);
        }

        private void MoveUpDefaultComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            Debug.Assert(_comparer is null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            var nodes = _nodes;

            while (nodeIndex > 0)
            {
                var parentIndex = GetParentIndex(nodeIndex);
                var parent = nodes[parentIndex];

                if (Comparer<TPriority>.Default.Compare(node.Priority, parent.Priority) < 0)
                {
                    nodes[nodeIndex] = parent;
                    nodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

            nodes[nodeIndex] = node;
        }

        private void MoveUpCustomComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            Debug.Assert(_comparer is not null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            var comparer = _comparer;
            var nodes = _nodes;

            while (nodeIndex > 0)
            {
                var parentIndex = GetParentIndex(nodeIndex);
                var parent = nodes[parentIndex];

                if (comparer.Compare(node.Priority, parent.Priority) < 0)
                {
                    nodes[nodeIndex] = parent;
                    nodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

            nodes[nodeIndex] = node;
        }

        private void MoveDownDefaultComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            Debug.Assert(_comparer is null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            var nodes = _nodes;
            var size = _size;

            int i;
            while ((i = GetFirstChildIndex(nodeIndex)) < size)
            {
                var minChild = nodes[i];
                var minChildIndex = i;

                var childIndexUpperBound = Math.Min(i + Arity, size);
                while (++i < childIndexUpperBound)
                {
                    var nextChild = nodes[i];
                    if (Comparer<TPriority>.Default.Compare(nextChild.Priority, minChild.Priority) < 0)
                    {
                        minChild = nextChild;
                        minChildIndex = i;
                    }
                }

                if (Comparer<TPriority>.Default.Compare(node.Priority, minChild.Priority) <= 0) break;

                // Move the minimal child up by one node and
                // continue recursively from its location.
                nodes[nodeIndex] = minChild;
                nodeIndex = minChildIndex;
            }

            nodes[nodeIndex] = node;
        }

        private void MoveDownCustomComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            Debug.Assert(_comparer is not null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            var comparer = _comparer;
            var nodes = _nodes;
            var size = _size;

            int i;
            while ((i = GetFirstChildIndex(nodeIndex)) < size)
            {
                // Find the child node with the minimal priority
                var minChild = nodes[i];
                var minChildIndex = i;

                var childIndexUpperBound = Math.Min(i + Arity, size);
                while (++i < childIndexUpperBound)
                {
                    var nextChild = nodes[i];
                    if (comparer.Compare(nextChild.Priority, minChild.Priority) < 0)
                    {
                        minChild = nextChild;
                        minChildIndex = i;
                    }
                }

                // Heap property is satisfied; insert node in this location.
                if (comparer.Compare(node.Priority, minChild.Priority) <= 0) break;

                nodes[nodeIndex] = minChild;
                nodeIndex = minChildIndex;
            }

            nodes[nodeIndex] = node;
        }

        private static IComparer<TPriority>? InitializeComparer(IComparer<TPriority>? comparer)
        {
            if (typeof(TPriority).IsValueType)
            {
                if (comparer == Comparer<TPriority>.Default) return null;

                return comparer;
            }

            // TODO https://github.com/dotnet/runtime/issues/10050: Update if this changes in the future.
            return comparer ?? Comparer<TPriority>.Default;
        }

        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof(PriorityQueueDebugView<,>))]
        public sealed class UnorderedItemsCollection : IReadOnlyCollection<(TElement Element, TPriority Priority)>,
            ICollection
        {
            internal readonly PriorityQueue<TElement, TPriority> _queue;

            internal UnorderedItemsCollection(PriorityQueue<TElement, TPriority> queue)
            {
                _queue = queue;
            }

            object ICollection.SyncRoot => this;
            bool ICollection.IsSynchronized => false;

            void ICollection.CopyTo(Array array, int index)
            {
                ArgumentNullException.ThrowIfNull(array);

                if (array.Rank != 1) throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, nameof(array));

                if (array.GetLowerBound(0) != 0) throw new ArgumentException(SR.Arg_NonZeroLowerBound, nameof(array));

                if (index < 0 || index > array.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), index,
                        SR.ArgumentOutOfRange_IndexMustBeLessOrEqual);

                if (array.Length - index < _queue._size) throw new ArgumentException(SR.Argument_InvalidOffLen);

                try
                {
                    Array.Copy(_queue._nodes, 0, array, index, _queue._size);
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(SR.Argument_InvalidArrayType, nameof(array));
                }
            }

            public int Count => _queue._size;

            IEnumerator<(TElement Element, TPriority Priority)> IEnumerable<(TElement Element, TPriority Priority)>.
                GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }


            public Enumerator GetEnumerator()
            {
                return new Enumerator(_queue);
            }


            public struct Enumerator : IEnumerator<(TElement Element, TPriority Priority)>
            {
                private readonly PriorityQueue<TElement, TPriority> _queue;
                private readonly int _version;
                private int _index;

                internal Enumerator(PriorityQueue<TElement, TPriority> queue)
                {
                    _queue = queue;
                    _index = 0;
                    _version = queue._version;
                    Current = default;
                }


                public void Dispose()
                {
                }


                public bool MoveNext()
                {
                    var localQueue = _queue;

                    if (_version == localQueue._version && (uint)_index < (uint)localQueue._size)
                    {
                        Current = localQueue._nodes[_index];
                        _index++;
                        return true;
                    }

                    return MoveNextRare();
                }

                private bool MoveNextRare()
                {
                    if (_version != _queue._version)
                        throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);

                    _index = _queue._size + 1;
                    Current = default;
                    return false;
                }


                public (TElement Element, TPriority Priority) Current { get; private set; }

                object IEnumerator.Current => Current;

                void IEnumerator.Reset()
                {
                    if (_version != _queue._version)
                        throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);

                    _index = 0;
                    Current = default;
                }
            }
        }
    }
}