using UnityEngine;

public class RangeAttack : AttackType
{
    public override void Enter() { }

    public override void Excute()
    {
        
        Debug.Log("range shoot");
        // ProjectileManager.instance.Generate(transform);
        // ProjectileManager.instance.Generate(transform.position, Quaternion.Euler(0, 30, 0));
        // ProjectileManager.instance.Generate(transform.position, Quaternion.Euler(0, 60, 0));
        ProjectileManager.Instance.Generate(transform);
    }
}