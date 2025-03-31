public class WarriorController: EnemyController
{
    private void Start()
    {
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new HitNode()),
            new SequenceNode(new IdleNode(), new PatrolNode()),
            new SequenceNode(new TracingNode(), new MeleeAttackNode())
        ));
    }
}