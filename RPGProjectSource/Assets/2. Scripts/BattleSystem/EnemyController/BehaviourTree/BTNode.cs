using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BTNode
{
    // 방문하는 정보
    protected EnemyController controller;
    protected NavMeshAgent agent;
    protected Transform transform;
    protected Transform target;
    
    // 노드 관련
    protected BehaviourTree behaviourTree;
    protected BlackBoard blackBoard;
    protected BTNode parent;
    public List<BTNode> childNodes { get; private set; } = new();
    
    
    public enum State { Success, Failure }
    public State? currentState = null;
    public Action<BTNode> notifyChangedByChildNode;

    public BTNode(params BTNode[] nodes)
    {
        foreach (BTNode node in nodes)
        {
            // 자식 요소에게 부모 노드 존재 알림
            node.SetParent(this);
            childNodes.Add(node);
        }
    }
    
    // 레지스터를 통해 해당 controller 정보 전달 받음(비지터 패턴)
    // OCP 관련 문제 발생
    public void Register(BehaviourTree behaviourTree)
    {
        this.behaviourTree = behaviourTree;
        this.blackBoard = behaviourTree.blackboard;
        
        controller = behaviourTree.controller;
        agent = controller.GetComponent<NavMeshAgent>();
        transform = controller.GetComponent<Transform>();
        target = controller.target;
    }
    
    private void SetParent(BTNode node) => parent = node;

    // 자신의 상태 저장
    // ReSharper disable Unity.PerformanceAnalysis
    public void SetState(State newState)
    {
        currentState = newState;
        parent?.notifyChangedByChildNode?.Invoke(this); // 부모노드가 분리되지 않으면서 문제 발생
        currentState = null; // 일시적 정보라 직접 가지는 것이 부적합할 수 있음
        
    }
    
    // 라이프사이클
    public virtual void Start() {}
    public virtual void Update() {}
    public virtual void End() {}
}

public class RootNode : BTNode
{
    public RootNode(params BTNode[] nodes) : base(nodes)
    { 
        // 루프를 돌고 다시 순회하는 상황
        notifyChangedByChildNode += (node) =>
        {
            // 여기서 직접 반복이 발생되면 무한 루프 에러가 발생한다.
            behaviourTree.isCycleEnd = true;
        };
    }

    public override void Start()
    {
        behaviourTree.ChangeCurrentNode(childNodes[0]);
    }
}

// 하나라도 실패한 경우, 즉시 실패
public class SequenceNode : BTNode
{
    public SequenceNode(params BTNode[] nodes) : base(nodes)
    {
        // 자식 노드로부터 받아서
        notifyChangedByChildNode += (currNode) =>
        {
            if (currNode.currentState == State.Failure)
            {
                behaviourTree.ChangeCurrentNode(null); // 선택된 노드가 없는 상황으로 간주, 일시 정지
                SetState(State.Failure);
                return;
            }

            int index = childNodes.IndexOf(currNode);
            
            // 모두 거친 경우 
            if (index == childNodes.Count - 1)
            {
                behaviourTree.ChangeCurrentNode(null);
                SetState(State.Success);
                return;
            }
            
            behaviourTree.ChangeCurrentNode(childNodes[index + 1]); // 다음 노드 저장
        };
    }
    
    public override void Start()
    {
        // 만약 조건적 확장이 필요하다면 override로 확장하기
        behaviourTree.ChangeCurrentNode(childNodes[0]);
    }
}

// // 성공한 경우, 다음으로 이동 X
public class SelectorNode : BTNode
{
    public SelectorNode(params BTNode[] nodes) : base(nodes)
    {
        notifyChangedByChildNode += (currNode) =>
        {
            // 실패했을 경우 다음 노드로 이동
            if (currNode.currentState == State.Failure)
            {
                int index = childNodes.IndexOf(currNode);
                if (index == childNodes.Count - 1) // 마지막 노드인 경우 실패 알림
                {
                    behaviourTree.ChangeCurrentNode(null); // 자식 노드 중에 더이상 진행중이 없음을 알림, 또한 이 노드가 진행중이지도 않음
                    SetState(State.Failure);
                    return;
                }
                
                behaviourTree.ChangeCurrentNode(childNodes[index + 1]);
                return;
            }

            // 성공했을 경우 성공을 알림
            if (currNode.currentState == State.Success)
            {
                SetState(State.Success);
            }
        };
    }
    
    public override void Start()
    {
        behaviourTree.ChangeCurrentNode(childNodes[0]);
    }
}