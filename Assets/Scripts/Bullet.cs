using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    private Rigidbody rbBullet;

    private void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rbBullet.MovePosition(rbBullet.position + transform.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
