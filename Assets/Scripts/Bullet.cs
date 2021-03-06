﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 50;
    private Rigidbody rbBullet;

    private float yPos;

    private void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        yPos = rbBullet.position.y;
        Destroy(gameObject, 10);
    }

    void FixedUpdate()
    {
        rbBullet.MovePosition(new Vector3(rbBullet.position.x, yPos, rbBullet.position.z) + transform.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Enemy":
                other.GetComponent<EnemyController>().TakeDamage(1);
                break;
            case "Boss":
                other.GetComponent<BossController>().TakeDamage(1);
                break;
        }

        Destroy(gameObject);
    }
}
