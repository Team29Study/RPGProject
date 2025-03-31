using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface IDamagable
{
    public void TakeDamage(int damage);
}

public class PlayerStat : MonoBehaviour , IDamagable
{
    public Player player;

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

    public void OnAttackMonster()
    {
        // 히트박스 생성
        ProjectileManager.Instance.CreateMeleeAttack(transform, true);
        HitBox hitBox = GetComponentInChildren<HitBox>();

        if(hitBox)
        {
            // 이벤트 등록
            //hitBox.onTrigger += ;
        }
    }

    public void OffAttackMonster()
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, false);

        HitBox hitBox = GetComponentInChildren<HitBox>();

        if(hitBox)
        {
            // 이벤트 해제
            //hitBox.onTrigger -=;
        }
    }

    public void Attack(Collider collider)
    {
        int attackDamage = player.Data.AttackData.AttackInfoDatas[player.stateMachine.ComboIndex].Damage;
        attackDamage += Atk;

        IDamagable enemy = collider.GetComponent<IDamagable>();

        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    public void BlockAttack()
    {
        // 투사체 또는 공격이 캐릭터의 앞쪽인지 판단해야함
        Transform attack = null;
        Vector3 attackDir = (attack.position - transform.position).normalized;
    }
}