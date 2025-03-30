using UnityEngine;

public class MeleeAttack : AttackType
{
    private GameObject hitBox;
    
    // 동적처리가 맞을 지 생각해보기
    public override void Enter()
    {
        // transform.GetComponentInChildren<HitBox>(true);
        hitBox = new GameObject("HitBox");
        hitBox.SetActive(false);
        hitBox.AddComponent<HitBox>();
        
        hitBox.transform.parent = transform;
        hitBox.transform.localPosition = new Vector3(0, 1, 1);

        hitBox.AddComponent<BoxCollider>().isTrigger = true;
    }

    public override void Excute()
    {
        hitBox.SetActive(true);
    }
}