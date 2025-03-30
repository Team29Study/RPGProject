using UnityEngine;

public class RogueController: EnemyController
{
    private void Start()
    {
        // behaviourTree.Generate(this, new SelectorNode(
        //     new SequenceNode(new HitNode(), new DieNode()),
        //     new SequenceNode(new IdleNode(), new PatrolNode()),
        //     new SequenceNode(new TracingNode(), new AttackNode(new RangeAttack()))
        // ));
        
        behaviourTree.Generate(this, new IdleNode());
    } 

    public void ShootArrow()
    {
        // Debug.Log("shoot arrow");
    }
}