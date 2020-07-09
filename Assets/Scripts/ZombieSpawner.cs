using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;
    private float timeCount = 0;
    public float TimeToSpawn = 1;

    void Start()
    {
        
    }
    
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount >= TimeToSpawn)
        {
            Instantiate(Zombie, transform.position, transform.rotation);
            timeCount = 0;
        }
    }
}
