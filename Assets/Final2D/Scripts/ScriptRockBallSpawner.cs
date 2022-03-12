using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRockBallSpawner : MonoBehaviour
{
    public GameObject RockBall;

    public float spawnTime = 5;

    void Awake()
    {
        StartCoroutine(SpawnRockBall(spawnTime));
    }

    IEnumerator SpawnRockBall(float spawnTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject clone = Instantiate(RockBall, transform.position, transform.rotation);
            Destroy(clone, 20);
        }
    }
}
