using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    private Player player;
    private PlayerStat playerStat;

    public bool isDie = false;
    public event Action onDie;

    private float hitDelayTime = 0.5f; // hit 딜레이 시간
    private float lastHitTime;      // 마지막으로 hit된 시간

    private void Awake()
    {
        player = GetComponent<Player>();
        playerStat = GetComponent<PlayerStat>();
        isDie = false;
    }

    public void TakeDamage(int damage, Transform attackTr = null)
    {
        if (Time.time - lastHitTime < hitDelayTime) return;

        lastHitTime = Time.time;

        Vector3 attackDir = attackTr ? (new Vector3(attackTr.position.x, 0, attackTr.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized : Vector3.zero;

        // 막기 불가능하다면 데미지를 입힘
        if (!PossibleBlock(attackDir))
            playerStat.HP = Mathf.Max(playerStat.HP - CalculateDef(damage), 0);

        if (playerStat.HP == 0)
        {
            isDie = true;
            onDie?.Invoke();
        }
    }

    private int CalculateDef(int damage)
    {
        return damage - playerStat.DefencePower;
    }

    // 가드가 가능한지 확인
    public bool PossibleBlock(Vector3 attackDir)
    {
        if (player.stateMachine.IsBlocking)
        {
            float dot = Vector3.Dot(transform.forward.normalized, attackDir.normalized);
            float degree = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (degree > 30)
            {
                Debug.Log("가드 실패");
                return false;
            }
            else
            {
                Debug.Log("가드 성공");
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void UseConsumableItem(ConsumableItemData itemData)
    {
        playerStat.HP += itemData.hpRecorvery;
    }
}
