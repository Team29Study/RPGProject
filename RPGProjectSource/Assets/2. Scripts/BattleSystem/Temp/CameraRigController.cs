// 단순 플레이어 추적

using UnityEngine;

public class CameraRigController: MonoBehaviour
{
    public Transform target;
    public float height;

    private void Update()
    {
        transform.position = new(target.position.x, height, target.position.z);
    }
}