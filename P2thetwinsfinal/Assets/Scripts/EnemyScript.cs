using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 enemyPosition;
    private string enemyState = "patrol";
    public Transform[] patrolPoints;
    private int currentPatrolPointIndex = 1;
    private float attackDistance = 2.0f;
    private float moveSpeed = 5.0f;

     private void Update()
    {
        // Update the enemy's position
        enemyPosition = transform.position;

        // Update the player's position
        Vector3 playerPosition = player.transform.position;

        // Determine the state of the enemy
        if (PlayerInRange(playerPosition))
        {
            enemyState = "attack";
        }
        else if (enemyState == "attack" && !PlayerInRange(playerPosition))
        {
            enemyState = "follow";
        }
        else if (enemyState == "follow" && !PlayerInRange(playerPosition))
        {
            enemyState = "patrol";
        }

        // Perform actions based on the enemy's state
        switch (enemyState)
        {
            case "attack":
                AttackPlayer(playerPosition);
                break;
            case "follow":
                FollowPlayer(playerPosition);
                break;
            case "patrol":
                Patrol();
                break;
        }
    }

    private bool PlayerInRange(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(enemyPosition, playerPosition);
        return distance <= attackDistance;
    }

    private void AttackPlayer(Vector3 playerPosition)
    {
        // Perform attack actions on the player
        Debug.Log("Attacking the player!");
    }

    private void FollowPlayer(Vector3 playerPosition)
    {
        // Rotate towards the player's position
        Vector3 direction = playerPosition - enemyPosition;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime);

        // Move towards the player's position
        Vector3 moveDirection = direction.normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Patrol()
    {
        // Check if there are no patrol points defined
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points defined for the enemy.");
            return;
        }

        // Move towards the current patrol point
        Vector3 currentPatrolPoint = patrolPoints[currentPatrolPointIndex].position;
        Vector3 direction = currentPatrolPoint - enemyPosition;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime);

        // Move towards the current patrol point
        Vector3 moveDirection = direction.normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Check if the enemy has reached the current patrol point
        float distanceToPatrolPoint = Vector3.Distance(enemyPosition, currentPatrolPoint);
        if (distanceToPatrolPoint <= 0.1f)
        {
            // Move to the next patrol point
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnMouseDown()
    {
        // Destroy the enemy when clicked
        Destroy(gameObject);
    }
    private void Start()
    {
        // Find the player GameObject with the "Player" tag
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player GameObject not found with the specified tag.");
        }
    }
}
