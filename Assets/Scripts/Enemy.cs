using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public int attackDamage = 5;
    [SerializeField] public float attackDelay = 1f;
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent navAgent;

    private NavMeshAgent agent;
    private Health targetHealth;
    private float targetDistance;
    private float updateTime = 0.2f;
    private float updateTimer;
    private bool isReadyToAttack;
    private float attackTimer;
    private float enemySpeed;

    void Start()
    {
        agent = gameObject.GetComponentOrNull<NavMeshAgent>();
        targetHealth = target.GetComponentOrNull<Health>();
    }

    private void FixedUpdate()
    {
        enemySpeed = agent.velocity.magnitude / agent.speed;
    }

    void Update()
    {
        animator.SetFloat("Speed", enemySpeed);
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateTime)
        {
            UpdateTarget();
            updateTimer = 0;
        }

        if (!isReadyToAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                isReadyToAttack = true;
                attackTimer = 0;
            }
        }

        if (isReadyToAttack)
        {
            targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance <= attackRange) Attack();
        }
    }

    void UpdateTarget()
    {
        agent.SetDestination(target.transform.position);
    }

    async void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
        Debug.Log("IsTransiction: "+animator.IsInTransition(0));

        targetHealth.TakeDamage(attackDamage);
        isReadyToAttack = false;
    }
}