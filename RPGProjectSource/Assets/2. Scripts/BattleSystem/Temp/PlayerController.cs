using UnityEngine;
using UnityEngine.InputSystem;

// 상하좌우 와 클릭
public class PlayerController: MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    
    private Vector2 moveDirection = Vector2.zero;
    public int moveSpeed = 8;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate((transform.forward * moveDirection.y + transform.right * moveDirection.x) * (Time.deltaTime * moveSpeed));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HitBox>())
        {
            Debug.Log("hitted");
            return;
        }
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        
        if(moveDirection == Vector2.zero) animator.SetBool(Run, false);
        else animator.SetBool(Run, true);
    }
    
    public void OnAttack(InputValue value)
    {
        animator.SetTrigger(Attack);
    }
}