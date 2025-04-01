using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public event Action<NPC> OnInteraction = delegate { };

    public void Interaction()
    {
        
    }
}
