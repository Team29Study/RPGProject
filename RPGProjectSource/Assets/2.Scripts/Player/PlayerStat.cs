using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface IDamagable
{
    // attackTr 근접 공격, 원거리 공격의 HitBox Transform
    public void TakeDamage(int damage, Transform attackTr = null);
}

public enum PlayerStatType
{
    AttakPower,     // 공격력
    DefencePower   // 방어력
}

public class PlayerStat : MonoBehaviour , IDamagable
{
    public Player player;

    private StatMediator<PlayerStatType> mediator = new();

    public int MaxHP { get; set; }
    [field: SerializeField]public int HP { get; set; }
    private int attackPower;
    public int AttackPower 
    { 
        get
        {
            var q = new ModifierQuery<PlayerStatType>(PlayerStatType.AttakPower, attackPower);
            mediator.PerformQuery(q);

            return (int)q.Value;
        }
        set => attackPower = value; 
    }
    public int defencePower;
    public int DefencePower
    {
        get
        {
            var q = new ModifierQuery<PlayerStatType>(PlayerStatType.DefencePower, defencePower);
            mediator.PerformQuery(q);

            return (int)q.Value;
        }
        set => defencePower = value;
    }

    private void Awake()
    {
        player = GetComponent<Player>();

        MaxHP = player.Data.StatData.MaxHP;
        HP = MaxHP;
        attackPower = 0;
        defencePower = player.Data.StatData.Def;
    }

    public void AddBuff(PlayerStatType statType, float buffValue, float time)
    {
        mediator.AddModifier(new StatModifier<PlayerStatType>(statType, new AddOperation(buffValue), time));
    }
    public void MulBuff(PlayerStatType statType, float buffValue, float time)
    {
        mediator.AddModifier(new StatModifier<PlayerStatType>(statType, new MultiplyOperation(buffValue), time));
    }

    private void Update()
    {
        mediator.Update(Time.deltaTime);
    }

    public void TakeDamage(int damage, Transform attackTr = null)
    {
        Vector3 attackDir = (new Vector3(attackTr.position.x, 0, attackTr.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;

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
        return damage - DefencePower;
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
            hitBox.onTrigger += Attack;
        }
    }

    public void OffAttackMonster()
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, false);

        HitBox hitBox = GetComponentInChildren<HitBox>();

        if(hitBox)
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
        attackDamage += AttackPower;

        IDamagable enemy = collider.GetComponent<IDamagable>();

        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }
}