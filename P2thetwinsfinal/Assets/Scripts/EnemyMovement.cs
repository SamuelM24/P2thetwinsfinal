using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private bool isPlayerAlive = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerAlive)
        {
            // Add your enemy movement logic here
            agent.SetDestination(player.position);
        }
        else
        {
            // Stop the enemy from moving
            agent.SetDestination(transform.position);
        }
    }

    public void SetPlayerAlive(bool alive)
    {
        isPlayerAlive = alive;
    }
}
