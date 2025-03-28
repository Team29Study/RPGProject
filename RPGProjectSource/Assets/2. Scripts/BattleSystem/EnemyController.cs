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
        if (other.GetComponent<HitBox>())
        {
            Debug.Log("enemy hitted box");
            
            // stateMachine.ChangeState(new HitState(stateMachine));
            return;
        }
    }
}