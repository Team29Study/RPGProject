using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface IDamagable
{
    // attackTr 근접 공격, 원거리 공격의 HitBox Transform
    public void TakeDamage(int damage, Transform attackTr = null);
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

    public void TakeDamage(int damage, Transform attackTr = null)
    {
        Vector3 attackDir = (attackTr.position - transform.position).normalized;

        // 막기 불가능하다면 데미지를 입힘
        if (!PossibleBlock(attackDir))
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

    // 가드가 가능한지 확인
    public bool PossibleBlock(Vector3 attackDir)
    {
        if (player.stateMachine.IsBlocking)
        {
            float dot = Vector3.Dot(transform.forward.normalized, attackDir.normalized);
            float degree = Mathf.Acos(dot) * Mathf.Rad2Deg;

            Debug.Log(degree);
            if (degree > 30)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
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

    public void BlockAttack(Collider collider)
    {
        // 투사체 또는 공격이 캐릭터의 앞쪽인지 판단해야함
        Transform attack = null;
        Vector3 attackDir = (attack.position - transform.position).normalized;
    }
}