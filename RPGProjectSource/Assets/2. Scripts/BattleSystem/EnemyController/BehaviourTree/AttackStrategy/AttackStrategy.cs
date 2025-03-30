using UnityEngine;

// 공격 전술 자체는 behaviour로 넘어가게 된다.
public class AttackStrategy: MonoBehaviour
{
    private AttackType attackType;

    public void Awake()
    {
        attackType = GetComponent<AttackType>();
    }

    public void Start()
    {
        attackType.Enter();
    }
    
    public void Attack()
    {
        attackType.Excute();
    }
}