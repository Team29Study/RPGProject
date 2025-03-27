using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 Component들을 관리하는 클래스
public class Player : MonoBehaviour
{
    [field:Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; } 

    public Animator Animator { get; private set; }
    public PlayerController InputController { get; private set; }
    public CharacterController CharController { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponent<Animator>();
        InputController = GetComponent<PlayerController>();
        CharController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
