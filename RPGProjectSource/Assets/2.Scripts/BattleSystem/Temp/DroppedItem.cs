using UnityEngine;

public class DroppedItem: MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(transform.Jump(2f, 0.5f));
        StartCoroutine(transform.RotateObject());
    }
}