using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineFreeLook cinemachineCamera;

    private float cameraYSpeed;
    private float cameraXSpeed;

    private bool isCameraMove;

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

        cameraYSpeed = cinemachineCamera.m_YAxis.m_MaxSpeed;
        cameraXSpeed = cinemachineCamera.m_XAxis.m_MaxSpeed;

        UIManager.Instance.cameraMove += ToggleCameraSpeed;
    }

    public void ToggleCameraSpeed()
    {
        isCameraMove = Cursor.lockState == CursorLockMode.Locked;

        if(isCameraMove)
        {
            cinemachineCamera.m_YAxis.m_MaxSpeed = cameraYSpeed;
            cinemachineCamera.m_XAxis.m_MaxSpeed = cameraXSpeed;
        }
        else
        {
            cinemachineCamera.m_YAxis.m_MaxSpeed = 0f;
            cinemachineCamera.m_XAxis.m_MaxSpeed = 0f;
        }
    }
}
