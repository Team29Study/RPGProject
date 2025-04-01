using JetBrains.Annotations;
using System;
using System.Collections.Generic;

public class BlackBoard
{
    public enum Trigger { Hit, Attack }
    
    [ItemCanBeNull] public Dictionary<Trigger, string> data = new();

    public BlackBoard()
    {
        foreach (Trigger value in Enum.GetValues(typeof(Trigger)))
        {
            data.Add(value, false.ToString());
        }
    }

    public void SetData(Trigger name, string value)
    {
        data[name] = value;
    }
}