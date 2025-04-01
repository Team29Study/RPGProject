using System;
using UnityEngine;

// 공격 타입이 아닌 공격 유효 체크 역할만 담당하도록 변경
public class HitBox: MonoBehaviour
{
    public event Action<Collider> onTrigger;
    public enum Caster { Player, Enemy }

    public Caster caster;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger?.Invoke(other);
    }
}    