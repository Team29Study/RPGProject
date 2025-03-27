using UnityEngine;

public abstract class AttackType : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Excute();
}

public class MeleeAttack : AttackType
{
    private GameObject hitBox;
    public override void Enter()
    {
        // transform.GetComponentInChildren<HitBox>(true);
        hitBox = new GameObject("HitBox");
        hitBox.SetActive(false);
        hitBox.AddComponent<HitBox>();
        
        hitBox.transform.parent = transform;
        hitBox.transform.localPosition = new Vector3(0, 1, 1);
    }

    public override void Excute() { }
}

