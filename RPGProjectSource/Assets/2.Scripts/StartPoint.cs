using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private Vector3 position;

    private TriggerArea triggerArea;

    void Awake() => triggerArea = GetComponent<TriggerArea>();

    void OnEnable() => triggerArea.OnTrigger += Warp;
    void OnDisable() => triggerArea.OnTrigger -= Warp;

    private void Warp(Collider col)
    {
        if (col.CompareTag(targetTag))
        {
            col.TryGetComponent<CharacterController>(out var handle);
            
            if (handle)
            {
                handle.enabled = false;
                handle.transform.position = position;
                handle.enabled = true;
            }
        }
    }
}
