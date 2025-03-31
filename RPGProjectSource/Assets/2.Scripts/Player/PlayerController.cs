using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public PlayerInputs PlayerInputs { get; private set; }
    public PlayerInputs.PlayerActions PlayerActions { get; private set; }

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
}
