using UnityEngine;

public class WarriorController: EnemyController
{
    public bool isDefenced = false;
    private void Start()
    {
        behaviourTree.Generate(this, new SelectorNode(
        new SequenceNode(new HitNode()),
        new SequenceNode(new IdleNode(), new PatrolNode()),
        new SequenceNode(new TracingNode(), new MeleeAttackNode())
        ));
        
        // behaviourTree.Generate(this, new DashAttackNode());
        // behaviourTree.Generate(this, new JumpAttackNode());
    }
    
    protected override void OnTriggerEnter(Collider other) // 방어중이면 Hit으로 처리 안함 
    {
        behaviourTree.notify(BlackBoard.Trigger.Hit, true.ToString());
    }

}