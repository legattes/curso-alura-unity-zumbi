using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController, IKillable
{
    public GameObject Player;

    private CharacterStatus status;

    private void Start()
    {
        status = GetComponent<CharacterStatus>();
        Player = GameObject.FindWithTag("Player");
        RandomSkin();
    }

    void FixedUpdate()
    {        
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 direction = Player.transform.position - transform.position;

        Rotate(direction);

        if (distance > 2)
        {
            Move(direction, status.Speed);

            AnimateBool("Attacking", false);
        } else
        {
            AnimateBool("Attacking", true);
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
