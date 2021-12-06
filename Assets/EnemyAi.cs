using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    public Vector3 walkPoint;

    //patroling
    bool walkPointSet;
    public float walkingPointRange;

    //Attacking
    public float attackDelay;
    bool isAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
      
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) Attacking();
    }

    private void Patroling()
    {
        if (!walkPointSet) SerchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;


    }

    private void SerchWalkPoint()
    {
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!isAttacked)
        {
            isAttacked = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
            
    }

    private void ResetAttack() 
    {
        isAttacked = false;
    }



}