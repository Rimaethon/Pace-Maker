using UnityEngine;

public class RectangleRotator : MonoBehaviour, IPooledObject
{
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, 3);
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
    }
}