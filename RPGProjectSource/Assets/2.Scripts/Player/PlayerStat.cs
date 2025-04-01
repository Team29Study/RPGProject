using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public Player player;
    public Transform attackPos;

    public int MaxHP { get; set; }
    public int HP { get; set; }
    public int Def { get; set; }
    // 추가 공격력
    public int Atk { get; set; } 

    private void Awake()
    {
        player = GetComponent<Player>();

        MaxHP = player.Data.StatData.MaxHP;
        HP = MaxHP;
        Def = player.Data.StatData.Def;
    }

    public void TakeDamage(int damage)
    {
        HP = Mathf.Max(HP - CalculateDef(damage), 0);

        if(HP == 0)
        {
            // 죽음
            Debug.Log("플레이어 사망");
        }
    }

    private int CalculateDef(int damage)
    {
        return damage - Def;
    }

    public void AttackMonster()
    {
        int attackDamage = player.Data.AttackData.AttackInfoDatas[player.stateMachine.ComboIndex].Damage;
        attackDamage += Atk;

        // 몬스터 레이어 필요
        Collider[] collider = Physics.OverlapSphere(attackPos.position, 1f);
        // 몬스터인지 판단
        // 데미지 입힘
        Debug.Log("공격! 데미지 : " + attackDamage);
    }

    public void BlockAttack()
    {
        // 투사체 또는 공격이 캐릭터의 앞쪽인지 판단해야함
        Transform attack = null;
        Vector3 attackDir = (attack.position - transform.position).normalized;
    }
}