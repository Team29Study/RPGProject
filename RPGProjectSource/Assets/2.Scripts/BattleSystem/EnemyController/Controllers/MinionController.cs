public class MinionController: EnemyController
{
    public float tracingRange;
    
    private void Start()
    {
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new DieNode(), new HitNode()),
            new SequenceNode(new TracingNode(tracingRange), new MeleeAttackNode()),
            new SequenceNode(new IdleNode(1, tracingRange), new PatrolNode(1))
        ));
    }
}