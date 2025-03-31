using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    private BTNode childNode;

    public enum State { Running, Success, Failure }
    public void SetNextNode(BTNode node) => childNode = node;
    
    public abstract State Execute();
}

// public class IdleNode : BTNode
// {
//     
// }

public class BlackBoard
{
    
}

public class SequenceNode : BTNode
{
    private List<BTNode> children;
    public SequenceNode(List<BTNode> children) { this.children = children; }
 
    public override State Execute()
    {
        foreach (var child in children)
        {
            State result = child.Execute();
            if (result == State.Failure)
                return State.Failure;
            if (result == State.Running)
                return State.Running;
        }
        return State.Success;
    }
}

// public class SelectorNode : BTNode
// {
//     
// }

public sealed class ActionNode : BTNode
{
    private Func<State> onUpdate;

    public ActionNode(Func<State> onUpdate)
    {
        this.onUpdate = onUpdate;
    }

    public override State Execute()
    {
        return onUpdate?.Invoke() ?? State.Failure;
    }
}