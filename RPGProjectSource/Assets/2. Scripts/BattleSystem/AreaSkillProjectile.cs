using UnityEngine;

public class AreaSkillProjectile: AttackType
{
    private GameObject hitBox;

    public override void Enter()
    {
        hitBox = new GameObject("HitBox");
        hitBox.SetActive(false);
        hitBox.AddComponent<HitBox>();
        
        hitBox.transform.parent = transform;
        hitBox.transform.localPosition = new Vector3(0, 1, 1);
    }

    public override void Excute()
    {
    }
}