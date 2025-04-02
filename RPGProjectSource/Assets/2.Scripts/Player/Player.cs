using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 Component들을 관리하는 클래스
public class Player : MonoBehaviour
{
    // 플레이어 상태별 데이터
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field:Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; } 

    public Animator Animator { get; private set; }
    public PlayerController InputController { get; private set; }
    public CharacterController CharController { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public PlayerStat PlayerStat { get; set; }
    public PlayerHealth PlayerHealth { get; set; }

    // 플레이어 상태 머신
    public PlayerStateMachine stateMachine;


    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponent<Animator>();
        InputController = GetComponent<PlayerController>();
        CharController = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        PlayerStat = GetComponent<PlayerStat>();
        PlayerHealth = GetComponent<PlayerHealth>();

        PlayerHealth.onDie += OnDie;

        // 상태머신 생성
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeMovementState(stateMachine.IdleState);
        stateMachine.ChangeAttackState(stateMachine.NoneAttackState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void OnDie()
    {
        Animator.SetTrigger("Death");
        enabled = false;
    }
}
