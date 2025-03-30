using UnityEngine;

public interface AttackType
{
    public void Execute(Transform transform);
}

public class MeleeAttack : AttackType
{
    public void Execute(Transform transform)
    {
    }
}

public class RangeAttack: AttackType
{
    public void Execute(Transform transform)
    {
        ProjectileManager.Instance.Generate(transform, HitBox.Caster.Enemy);
    }
}
