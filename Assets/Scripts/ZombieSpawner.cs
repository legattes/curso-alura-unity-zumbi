using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;
    public float TimeToSpawn = 5;
    public LayerMask ZombieLayer;
    public float RangeRandomPosition = 15;
    public float RangePlayerPostion = 20;

    private float timeCount = 0;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > RangePlayerPostion)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= TimeToSpawn)
            {
                StartCoroutine(GenerateZombie());
                timeCount = 0;
            }
        }
    }

    IEnumerator GenerateZombie()
    {
        Vector3 createPosition = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(createPosition, 2, ZombieLayer);

        while(colliders.Length > 0)
        {
            createPosition = RandomPosition();
            colliders = Physics.OverlapSphere(createPosition, 2, ZombieLayer);
            yield return null;
        }

        Instantiate(Zombie, createPosition, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangeRandomPosition);
    }

    Vector3 RandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * RangeRandomPosition;
        position += transform.position;
        position.y = 0;

        return position;
    }
}
