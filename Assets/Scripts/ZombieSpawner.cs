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
    private int maxZombieCount = 10;
    private int zombieCount;

    private float timeToIncreaseDificulty = 10;
    private float timeDificultyCount;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timeDificultyCount = timeToIncreaseDificulty;
        StartCoroutine(GenerateZombie());
    }
    
    void Update()
    {

        bool hasDistanceToSpawn = Vector3.Distance(transform.position, player.transform.position) > RangePlayerPostion;
        bool hasReachedMaxZombies = zombieCount < maxZombieCount;

        if (hasDistanceToSpawn && hasReachedMaxZombies)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= TimeToSpawn)
            {
                StartCoroutine(GenerateZombie());
                timeCount = 0;
            }
        }

        if(Time.timeSinceLevelLoad > timeDificultyCount)
        {
            maxZombieCount += 10;
            timeDificultyCount = Time.timeSinceLevelLoad + timeToIncreaseDificulty;
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

        EnemyController zombie = Instantiate(Zombie, createPosition, transform.rotation).GetComponent<EnemyController>();
        zombie.spawner = this;

        zombieCount++;
    }

    public void decreaseZombieCounter()
    {
        zombieCount--;
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
        position.y = 1.05f;

        return position;
    }
}
