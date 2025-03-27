// 단순 플레이어 추적

using UnityEngine;

public class CameraRigController: MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}