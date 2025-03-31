using UnityEngine;

// 공격 타입이 아닌 공격 유효 체크 역할만 담당하도록 변경
public class HitBox: MonoBehaviour
{
    public enum Caster { Player, Enemy }

    public Caster caster;
}    