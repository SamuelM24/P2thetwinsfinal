using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float movementSpeed = 5f;

    void Start()
    {
        enemy.speed = movementSpeed;
        enemy.updateRotation = false; // Disable rotation update
    }

    void Update()
    {
        enemy.SetDestination(player.position);
    }
}
