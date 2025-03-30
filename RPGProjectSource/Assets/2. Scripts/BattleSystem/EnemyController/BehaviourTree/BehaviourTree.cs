using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BehaviourTree
{ 
    public EnemyController controller {get; private set;}
    
    private BTNode rootNode;
    public BTNode currentnode {get; private set;}
    public BlackBoard blackboard { get; private set; } = new();
    
    // status
    public bool isCycleEnd = true;
    public bool isRunning = true;
    
    public void Generate(EnemyController controller, BTNode startNode)
    {
        this.controller = controller;
        this.rootNode = new RootNode(startNode);
        Register(this.rootNode);
    }
    
    // null인 경우 체크 필요
    public void ChangeCurrentNode(BTNode node) // 라이프사이클도 동시에 발생
    {
        isRunning = false;
        currentnode?.End();
        currentnode = node;
        currentnode?.Start(); // 스타트에서 바로 변경한다면 이 곳에 그 로직이 들어간다고 생각하면 된다.
        isRunning = true;
    }



    // 재귀를 통해 일관 전달
    public void Register(BTNode node)
    {
        node.Register(this);
        
        node.childNodes.ForEach(childNode =>
        {
            Register(childNode);
        });
    }
    
    // 특수한 경우 처음부터 다시 시작해야한다.
    public void Reset()
    {
        ChangeCurrentNode(rootNode);
    }
    // operator는 Update에서 실행된다.
    // ReSharper disable Unity.PerformanceAnalysis
    public void Run()
    {
        if (isCycleEnd)
        {
            isCycleEnd = false;
            ChangeCurrentNode(rootNode);
        }
        if (isRunning) { currentnode?.Update(); }
    }

    public void notify(BlackBoard.Trigger name, string value)
    {
        blackboard.SetData(name, value);
        Reset();
    }
}