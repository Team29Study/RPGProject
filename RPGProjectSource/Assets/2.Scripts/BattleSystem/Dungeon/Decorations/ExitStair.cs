using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStair: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("VillageScene"); // 위치 설정 어떻게?
        }
    }
}