using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IObservableArray<T>
{
    event Action<T[]> AnyValueChanged;

    int Count { get; }
    T this[int index] { get; }

    void Swap(int index1, int index2);
    void Clear();
    bool TryAdd(T item);
    bool TryAddAt(int index, T item);
    bool TryRemove(T item);
    bool TryRemoveAt(int index);
}

public class ObserverArray<T> : IObservableArray<T>
{
    public T[] items;

    public T this[int index] => items[index];
    public int Count => items.Length;
    public event Action<T[]> AnyValueChanged = delegate { };

    public ObserverArray(int capacity = 1, IList<T> initialList = null)
    {
        items = new T[capacity];

        if (initialList != null)
        {
            initialList.Take(capacity).ToArray().CopyTo(items, 0);
            Invoke();
        }
    }

    public void Clear()
    {
        items = new T[items.Length];
        Invoke();
    }

    public void Swap(int index1, int index2)
    {
        (items[index1], items[index2]) = (items[index2], items[index1]);
        Invoke();
    }

    public bool TryAdd(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (TryAddAt(i, item)) return true;
        }

        return false;
    }

    public bool TryAddAt(int index, T item)
    {
        if (index < 0 || index >= Count) return false;

        if (items[index] != null) return false;

        items[index] = item;
        Invoke();

        return true;
    }

    public bool TryRemove(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(items[i], item) && TryRemoveAt(i)) return true;
        }
        return false;
    }

    public bool TryRemoveAt(int index)
    {
        if (index < 0 || index >= Count) return false;

        if (items[index] == null) return false;

        items[index] = default;
        Invoke();
        return false;
    }

    public void Invoke() => AnyValueChanged?.Invoke(items);
}
