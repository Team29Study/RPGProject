using UnityEngine;

public class Decoration : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
