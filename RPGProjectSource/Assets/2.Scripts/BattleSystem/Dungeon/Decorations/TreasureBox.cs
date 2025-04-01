using System;
using System.Collections;
using UnityEngine;

// 자식 요소중 상단부 회전 발생 후 완료되면 삭제 및 아이템 추가
public class TreasureBox: MonoBehaviour
{
    private bool isGetting = false;
    public Transform topBox;
    
    public void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player") || isGetting) return;
        isGetting = true;
        StartCoroutine(GetReward());
    }

    IEnumerator GetReward()
    {
        while (!Mathf.Approximately(topBox.localRotation.eulerAngles.x, 270))
        {
            topBox.localRotation = Quaternion.RotateTowards(topBox.localRotation, Quaternion.Euler(270, 0, 0), 300f * Time.deltaTime);
            yield return null;
        }
        
        Debug.Log("get reward");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}