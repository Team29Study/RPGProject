using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Event/BoundedValue")]
public class BoundedValueGameEvent : GameEvent<BoundedValue>
{

}

[System.Serializable]
public struct BoundedValue
{
    public readonly int value;
    public readonly int maxValue;

    public BoundedValue(int value, int maxValue)
    {
        this.value = value;
        this.maxValue = maxValue;
    }

    public float Ratio => (float)value / (float)maxValue;
}

