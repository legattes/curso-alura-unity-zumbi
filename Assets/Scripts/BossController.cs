using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : CharacterBaseController, IKillable
{
    private Transform player;
    private NavMeshAgent agent;
    private CharacterStatus status;
    private BossAnimator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<CharacterStatus>();
        animator = GetComponent<BossAnimator>();
        agent.speed = status.Speed;
    }

    void Update()
    {
        agent.SetDestination(player.position);
        animator.Walk(agent.velocity.magnitude);

        if (agent.hasPath)
        {
            bool nearPlayer = agent.remainingDistance <= agent.stoppingDistance;
            Rotate(player.position - transform.position);
            animator.Attack(nearPlayer);
        }
    }

    void Attack()
    {
        int damage = Random.Range(30, 50);
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        status.Life -= damage;
        if(status.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.Die();
        agent.enabled = false;
        this.enabled = false;
        Destroy(gameObject, 5);
    }
}
