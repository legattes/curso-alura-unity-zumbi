using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController, IKillable
{
    public GameObject Player;

    private CharacterStatus status;
    private Vector3 randomPosition;
    private Vector3 direction;
    private float roamCount;
    private float timeBetweenPositions = 5;
    private int rangeRandomPosition = 15;

    private void Start()
    {
        status = GetComponent<CharacterStatus>();
        Player = GameObject.FindWithTag("Player");
        RandomSkin();
    }

    void FixedUpdate()
    {        
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Rotate(direction);
        AnimateFloat("Moving", direction.magnitude);

        if(distance > 15)
        {
            Roam();
        }
        else if (distance > 2)
        {
            Hunt();
        } else
        {
            Attack();
        }
    }

    void AtacaJogador()
    {
        int damage = Random.Range(20, 30);
        Player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    void RandomSkin()
    {
        int zombieType = Random.Range(1, 27);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void Roam()
    {
        roamCount -= Time.deltaTime;
        if(roamCount <= 0)
        {
            randomPosition = RandomPosition();
            roamCount += timeBetweenPositions;
        }

        if(Vector3.Distance(transform.position, randomPosition) >= 0.1)
        {
            direction = randomPosition - transform.position;
            Move(direction, status.Speed);
        } else if(roamCount > 3)
        {
            roamCount = 0;
        }
    }

    void Hunt()
    {
        direction = Player.transform.position - transform.position;

        Move(direction, status.Speed);

        AnimateBool("Attacking", false);
    }

    void Attack()
    {
        direction = Player.transform.position - transform.position;

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
    }
}
