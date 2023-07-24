using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

public class RelativePathFinder : MonoBehaviour
{
    // Reference to the asset you want to find the relative path for.
    [SerializeField]
    private Object targetAsset;

    private void Start()
    {
        FindRelativePath();
    }

    // Method to find the relative path of the asset and log it.
    [ContextMenu("Find Relative Path")]
    private void FindRelativePath()
    {
        string assetPath = AssetDatabase.GetAssetPath(targetAsset);
        if (!string.IsNullOrEmpty(assetPath))
        {
            string projectPath = Application.dataPath;
            int index = assetPath.IndexOf("Assets", StringComparison.Ordinal);
            if (index != -1)
            {
                string relativePath = assetPath.Substring(index);
                Debug.Log("Relative path of " + targetAsset.name + ": " + relativePath);
            }
            else
            {
                Debug.LogError("Asset not in the 'Assets' folder.");
            }
        }
        else
        {
            Debug.LogError("Invalid asset or asset not found.");
        }
    }
}