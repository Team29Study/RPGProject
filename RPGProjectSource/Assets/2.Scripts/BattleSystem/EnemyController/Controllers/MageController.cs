
public class MageController: EnemyController
{
    private void Start()
    {
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new DieNode(), new HitNode()),
            new SequenceNode(new TracingNode(16), new MageAttack()),
            new SequenceNode(new IdleNode(1, 16), new PatrolNode(1))
        ));
    } 
}