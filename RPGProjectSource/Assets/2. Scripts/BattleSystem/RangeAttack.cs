public class RangeAttack : AttackType
{
    public override void Enter() { }

    public override void Excute()
    {
        
        ProjectileManager.instance.Generate(transform);
    }
}