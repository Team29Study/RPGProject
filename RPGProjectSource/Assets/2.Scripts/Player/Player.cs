using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 플레이어의 Component들을 관리하는 클래스
public class Player : MonoBehaviour
{
    // 플레이어 상태별 데이터
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field:Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [Header("Interaction")]
    [SerializeField] private LayerMask interactionLayers;
    [SerializeField] private float rangeRadius = 2f;

    public Animator Animator { get; private set; }
    public PlayerController InputController { get; private set; }
    public CharacterController CharController { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public PlayerStat PlayerStat { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public Inventory Inventory { get; private set; }

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
        Inventory = GetComponent<Inventory>();

        PlayerHealth.onDie += OnDie;

        // 상태머신 생성
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        stateMachine.ChangeMovementState(stateMachine.IdleState);
        stateMachine.ChangeAttackState(stateMachine.NoneAttackState);

        InputController.PlayerActions.Interaction.started += (ctx) => OnInteraction();
        InputController.PlayerActions.Inventory.started += (ctx) =>
        {
            var inventoryUI = UIManager.Instance.GetUI<InventoryView>();
            Debug.Log(inventoryUI);
            inventoryUI?.Toggle();

        };
        InputController.PlayerActions.Equipment.started += (ctx) =>
        {
            var equipmentUI = UIManager.Instance.GetUI<EquipmentView>();

            equipmentUI?.Toggle();
        };
        InputController.PlayerActions.Pause.started += (ctx) =>
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ?
            CursorLockMode.None : CursorLockMode.Locked;
        };
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

    private void OnInteraction()
    {
        var others = Physics.OverlapSphere(transform.position, rangeRadius, interactionLayers,
            QueryTriggerInteraction.Collide);

        if (others.Length != 0)
        {
            foreach (var iter in others)
            {
                if (iter.TryGetComponent(out IInteractable<Player> interactable))
                {
                    interactable.Interaction(this);
                    return;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
