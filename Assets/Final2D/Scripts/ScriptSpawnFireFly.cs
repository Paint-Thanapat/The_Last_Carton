using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawnFireFly : MonoBehaviour
{
    public GameObject FireFlyPrefabs;

    private Transform player;

    Vector3 randomSpawn;

    private float ranX, ranY;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine("spawnFireFly");
    }

    void Update()
    {
        randomSpawn = new Vector3(player.transform.position.x + ranX, player.transform.position.y + ranY, player.transform.position.z);
    }

    IEnumerator spawnFireFly()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            ranX = Random.Range(-3, 4);
            ranY = Random.Range(-3, 4);
            GameObject FireFly = Instantiate(FireFlyPrefabs,randomSpawn, transform.rotation);
        }
    }
}
