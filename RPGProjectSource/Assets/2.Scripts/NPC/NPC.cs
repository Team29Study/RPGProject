using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable<Player>
{
    public event Action<NPC> OnInteraction = delegate { };

    public virtual void Interaction(Player player)
    {
        OnInteraction?.Invoke(this);
    }
}
