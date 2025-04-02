using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs PlayerInputs { get; private set; }
    public PlayerInputs.PlayerActions PlayerActions { get; private set; }

    [SerializeField] private LayerMask groundMask;
    private float rayDistance = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        PlayerInputs = new PlayerInputs();
        // InputActions의 Player 맵에 접근
        PlayerActions = PlayerInputs.Player;
    }

    private void OnEnable()
    {
        PlayerInputs.Enable();
    }

    private void OnDisable()
    {
        PlayerInputs.Disable();
    }

    // 떨어질만한 높이인지 확인
    public bool CheckFallHeight()
    {
        Ray ray = new Ray(transform.position, Vector3.down * rayDistance);

        // 떨어지지 않아도 괜찮은 높이라면 false
        if (Physics.Raycast(ray, rayDistance, groundMask))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * rayDistance);
    }
}
