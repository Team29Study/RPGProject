using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerStat playerStat;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerStat = GetComponent<PlayerStat>();
    }

    public void OnAttackMonster()
    {
        // 히트박스 생성
        ProjectileManager.Instance.CreateMeleeAttack(transform, true);
        HitBox hitBox = GetComponentInChildren<HitBox>();

        if (hitBox)
        {
            // 이벤트 등록
            hitBox.onTrigger += Attack;
        }
    }

    public void OffAttackMonster()
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, false);

        HitBox hitBox = GetComponentInChildren<HitBox>();

        if (hitBox)
        {
            // 이벤트 해제
            hitBox.onTrigger -= Attack;
        }
    }

    public void Attack(Collider collider)
    {
        // 공격의 기본 데미지
        int attackDamage = player.Data.AttackData.AttackInfoDatas[player.stateMachine.ComboIndex].Damage;
        // 캐릭터의 공격력 추가
        attackDamage += playerStat.AttackPower;

        IDamagable enemy = collider.GetComponent<IDamagable>();

        Debug.Log(collider.name);

        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }
}
