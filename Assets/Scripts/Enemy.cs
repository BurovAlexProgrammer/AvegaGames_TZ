using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public int attackDamage = 5;
    [SerializeField] public float attackDelay = 1f;
    [SerializeField] [Range(0f, 1f)] public float playBreatheChance = 0.5f;
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent navAgent;
    [SerializeField] public SimpleAudioEvent attackAudioEvent;
    [SerializeField] public SimpleAudioEvent breatheAudioEvent;
    [SerializeField] public SimpleAudioEvent deathAudioEvent;
    

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
        PlayBreatheSound();
    }

    private void OnDestroy()
    {
        gameObject.CreateAudioEvent(deathAudioEvent);
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
            if (targetDistance <= attackRange) PlayAttack();
        }
    }

    void UpdateTarget()
    {
        agent.SetDestination(target.transform.position);
    }

    void PlayAttack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
        isReadyToAttack = false;
    }

    [UsedImplicitly]
    void DamageTarget()
    {
        targetHealth.TakeDamage(attackDamage);
    }

    [UsedImplicitly]
    void PlayAttackSound()
    {
        gameObject.CreateAudioEvent(attackAudioEvent);
    }

    void PlayBreatheSound()
    {
        if (Random.value > playBreatheChance) return;
        gameObject.CreateAudioEvent(breatheAudioEvent, false);
    }
}