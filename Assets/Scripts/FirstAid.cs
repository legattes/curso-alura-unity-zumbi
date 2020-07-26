using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    private int healingAmount = 15;
    private int timeSelfDestroy = 10;

    private void Start()
    {
        Destroy(gameObject, timeSelfDestroy);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<PlayerController>().Heal(healingAmount);
            Destroy(gameObject);
        }
    }
}
