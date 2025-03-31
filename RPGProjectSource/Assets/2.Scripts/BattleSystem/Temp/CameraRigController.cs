// 단순 플레이어 추적

using UnityEngine;

public class CameraRigController: MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        transform.position = target.position;
    }
}