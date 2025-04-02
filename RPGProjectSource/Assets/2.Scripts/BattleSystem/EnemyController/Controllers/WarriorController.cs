using UnityEngine;

public class WarriorController: EnemyController
{
    public bool isDefenced = false;
    public float tracingRange;

    private void Start()
    {
        HitBox hitBox = ProjectileManager.Instance.RegisterMeleeAttack(transform, Vector3.up + Vector3.forward * 1.6f,  Vector3.one);
        hitBox.onTrigger += (other) =>
        {
            if (!other.TryGetComponent(out PlayerHealth damagable)) return;
            
            damagable.TakeDamage(resourceHandler.attack, transform);
            hitBox.gameObject.SetActive(false);
        };
        
        ProjectileManager.Instance.RegisterMeleeAttack(transform, Vector3.up + Vector3.forward,  Vector3.one * 2);
        
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new DieNode(), new HitNode(true)),
            new SequenceNode(new TracingNode(tracingRange), new RushAttackMode(), new DefenceNode()),
            new SequenceNode(new IdleNode(1, tracingRange), new PatrolNode(1))
        ));
    }
    
    // public override void TakeDamage(int damage, Transform attackTr = null) {}
}