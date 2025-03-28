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
    
    // 중력 수동 구현
    public float gravity = -9.81f;
    public Vector3 velocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            transform.position += transform.forward * (moveSpeed * Time.deltaTime);
        }
        
        // 중력 적용
        if (transform.position.y > 0f)
        {
            transform.position -= Vector3.down * (gravity * Time.deltaTime);
        }
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


        if (moveDirection == Vector2.zero)
        {
            animator.SetBool(Run, false);
        }
        else
        {
            animator.SetBool(Run, true);

            
            Debug.Log(moveDirection);
            Vector3 moveVector = Vector3.forward * moveDirection.y + Vector3.right * moveDirection.x;
            transform.rotation = Quaternion.LookRotation(moveVector);

        }
    }
    
    public void OnAttack(InputValue value)
    {
        animator.SetTrigger(Attack);
        ProjectileManager.instance.Generate(transform);
    }
}