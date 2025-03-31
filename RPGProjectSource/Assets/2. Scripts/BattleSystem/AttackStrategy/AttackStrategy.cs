using UnityEngine;

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