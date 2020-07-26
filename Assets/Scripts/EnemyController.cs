using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBaseController, IKillable
{
    public GameObject Player;
    public GameObject Medkit;
    public ZombieSpawner spawner;

    private CharacterStatus status;
    private Vector3 randomPosition;
    private Vector3 direction;
    private float roamCount;
    private float timeBetweenPositions = 5;
    private int rangeRandomPosition = 15;
    private float medkitSpawnChance = 0.1f;

    private float distance;

    private void Start()
    {
        status = GetComponent<CharacterStatus>();
        Player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {        
        distance = Vector3.Distance(transform.position, Player.transform.position);


        if(distance > 20)
        {
            Roam();
        }
        else if (distance > 3)
        {
            Hunt();
        } else
        {
            Attack();
        }
    }

    void AttackPlayer()
    {
        int damage = Random.Range(20, 30);
        Player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    void Roam()
    {
        roamCount -= Time.deltaTime;
        if(roamCount <= 0)
        {
            randomPosition = RandomPosition();
            roamCount += timeBetweenPositions + Random.Range(-2f, 2f);
        }

        if(Vector3.Distance(transform.position, randomPosition) >= 0.1)
        {
            direction = randomPosition - transform.position;
            Move(direction, status.Speed);

            Rotate(direction);
        } else if(roamCount > 3)
        {
            roamCount = 0;
        }
    }

    void Hunt()
    {
        direction = Player.transform.position - transform.position;

        Rotate(direction);
        AnimateFloat("Moving", direction.magnitude);

        Move(direction, status.Speed);

        AnimateBool("Attacking", false);
    }

    void Attack()
    {
        direction = Player.transform.position - transform.position;

        Rotate(direction);

        AnimateBool("Attacking", true);
    }

    Vector3 RandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * rangeRandomPosition;
        position += transform.position;
        position.y = transform.position.y;

        return position;
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
        Destroy(gameObject);
        ShouldSpawnMedkit(medkitSpawnChance);
        spawner.decreaseZombieCounter();
    }

    void ShouldSpawnMedkit(float percent)
    {
        if(Random.value <= percent)
        {
            Instantiate(Medkit, transform.position, Quaternion.identity);
        }
    }
}
