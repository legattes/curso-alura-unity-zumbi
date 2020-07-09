using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 5;
    private Rigidbody rbEnemy;
    private Animator animEnemy;

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        animEnemy = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        int zombieType = Random.Range(1, 27);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {        
        float distance = Vector3.Distance(rbEnemy.position, Player.transform.position);

        Vector3 direction = Player.transform.position - rbEnemy.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rbEnemy.MoveRotation(rotation);

        if (distance > 2)
        {
            rbEnemy.MovePosition(rbEnemy.position + direction.normalized * Speed * Time.deltaTime);
            animEnemy.SetBool("Attacking", false);
        } else
        {
            animEnemy.SetBool("Attacking", true);
        }
    }

    void AtacaJogador()
    {
        int damage = Random.Range(20, 30);
        Player.GetComponent<PlayerController>().TakeDamage(damage);
    }


}
