using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> listeners = new();

    public void Raise(T data) => listeners.ForEach(l => l.OnEventRaised(data));

    public void RegisterListener(IGameEventListener<T> listener) => listeners.Add(listener);
    public void DeregisterListener(IGameEventListener<T> listener) => listeners.Remove(listener);
}
