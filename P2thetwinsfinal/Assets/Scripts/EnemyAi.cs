using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public float maxHealth;
    public float rotationSpeed = 5f;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private PlayerHealth playerHealth;
    private int damageAmount = 10;

    private float attackTimer = 0f;
    public float attackDuration = 1f;
    public int damagePerSecond = 10;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        maxHealth = health;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance <= attackRange)
        {
            // Start moving towards the player immediately
            agent.SetDestination(player.position);
        }
        else if (playerDistance <= sightRange)
        {
            // Start moving towards the player within attack range
            Vector3 targetPosition = player.position + (transform.position - player.position).normalized * (playerDistance - attackRange);
            agent.SetDestination(targetPosition);
        }

        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Start attack
            attackTimer = 0f;
            alreadyAttacked = true;
        }

        // Gradually decrease player's health over time during the attack
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDuration)
        {
            int damageAmount = damagePerSecond * (int)attackDuration;
            playerHealth.TakeDamage(damageAmount);
            alreadyAttacked = false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Enemy death logic
        Destroy(gameObject);

        // Check if the player is dead and disable the enemy AI
        if (playerHealth.currentHealth <= 0)
        {
            DisableAI();
        }
    }

    private void DisableAI()
    {
        // Disable the NavMeshAgent component to stop the enemy from moving
        agent.enabled = false;

        // Disable the EnemyAi script component to stop the enemy's AI behavior
        this.enabled = false;
    }
}