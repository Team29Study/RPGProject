using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTester : MonoBehaviour
{
    public StatMediator<StatType> Mediator { get; private set; } = new();

    public float attackPower = 10;

    public float attackPowerTest;

    public float AttackPower
    {
        get
        {
            var q = new ModifierQuery<StatType>(StatType.AttackPower, attackPower);
            Mediator.PerformQuery(q);

            return q.Value;
        }
    }

    void Update()
    {
        Mediator.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.S))
        {
            Mediator.AddModifier(new StatModifier<StatType>(StatType.AttackPower, new AddOperation(5),
                5));
        }

        attackPowerTest = AttackPower;
    }
}

public enum StatType
{
    AttackPower
}