using UnityEngine;
public class RushAttack : AttackType
{
    private GameObject hitBox;
    
    // 동적처리가 맞을 지 생각해보기
    public override void Enter()
    {
        hitBox = new GameObject("HitBox");
        hitBox.SetActive(false);
        hitBox.AddComponent<HitBox>();
        
        hitBox.transform.parent = transform; // 몸통 박치기의 형ㅌ

        hitBox.AddComponent<BoxCollider>().isTrigger = true;
    }

    public override void Excute()
    {
        hitBox.SetActive(true);
        transform.position += Vector3.up * 1.5f; // 돌진
    }
}