using System;
using UnityEngine;
// 공격 범위(무기 자체, 혹은 히트박스 자체를 생성)
// 중복 초기화 필요(총알이라면?)
// hitType을 통해서 방식이 달라질 수 있음( 전략 패턴

public class HitBox : MonoBehaviour
{
    public string owner; // 시전 대상
    
    // public float explodePower = 3f;
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log(1 / (transform.position - other.transform.position).magnitude); // 폭팔 정도
    //         // 로봇 친구가 콜라이더 가지고 있음
    //         other.transform.parent.transform.position += Vector3.up * 2f + Vector3.back * 3f;
    //     }
    // }
}