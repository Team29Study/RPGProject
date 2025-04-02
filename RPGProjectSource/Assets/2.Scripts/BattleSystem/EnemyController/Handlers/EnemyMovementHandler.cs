using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementHandler: MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // enable Agent
}