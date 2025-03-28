using UnityEngine;

public class EnemyController: MonoBehaviour
{
    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HitBox hitBox))
        {
            if (hitBox.owner == "enemy") return;
            
            // stateMachine.ChangeState(new HitState(stateMachine));
        }
    }
}