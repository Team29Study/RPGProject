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

public class PlayerStat : MonoBehaviour
{
    public Player player;

    private StatMediator<PlayerStatType> mediator = new();

    public int MaxHP { get; set; }
    [SerializeField]
    private int hp;
    public int HP
    {
        get => hp;
        set => hp = Mathf.Min(value, MaxHP);
    }
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

    public void AddModifier(StatModifier<PlayerStatType> modifier)
    {
        mediator.AddModifier(modifier);
    }

    private void Update()
    {
        mediator.Update(Time.deltaTime);
    }
}