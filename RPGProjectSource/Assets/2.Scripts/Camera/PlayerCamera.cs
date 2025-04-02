using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineFreeLook cinemachineCamera;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineFreeLook>();

        if(!Camera.main.GetComponent<CinemachineBrain>())
        {
            Camera.main.AddComponent<CinemachineBrain>();
        }

        Player player = FindObjectOfType<Player>();

        cinemachineCamera.Follow = player.transform;
        cinemachineCamera.LookAt = player.transform.GetChild(1);
    }
}
