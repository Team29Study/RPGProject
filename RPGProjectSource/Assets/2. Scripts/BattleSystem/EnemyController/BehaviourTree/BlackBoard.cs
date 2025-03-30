using System.Collections.Generic;

public class BlackBoard
{
    private BehaviourTree behaviourTree;
    public Dictionary<string, string> data = new();

    public BlackBoard(BehaviourTree behaviourTree)
    {
        this.behaviourTree = behaviourTree;
        data.Add("HIT", false.ToString());
    }
    
    public void AddData(List<(string name, string value)> newData)
    {
        newData.ForEach(data =>
        { 
            this.data.Add(data.name, data.value);
        });
    }

    public void SetData(string name, string value)
    {
        data[name] = value;
    }

    public void Notify(string name, string value)
    {
        SetData(name, value);
        behaviourTree.Reset();
    }
}