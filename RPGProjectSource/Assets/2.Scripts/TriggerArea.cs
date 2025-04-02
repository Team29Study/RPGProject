using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public event Action<Collider> OnTrigger = delegate { };

    void OnTriggerEnter(Collider other)
    {
        OnTrigger?.Invoke(other);    
    }
}
