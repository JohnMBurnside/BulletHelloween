using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    float resetAfterTimer;
    public float resetEvery;
    public bool resetAfterTime;
    public bool restartAfterPlayerDeath = true;

    public GameObject enemy;
    //public GameObject spawnSound;
    //public float spawnDistance = 3.0f;
    public Vector3 spawnPosition;
    float time;
    float time2;
    float time3;
    //float timeFrame1 = 6.0f;
    public float timeTillStart = 30.0f;
    public float spawnDelay = 0.2f;
    public float spawnRangeY = 1.5f;
    public float spawnRangeX = 1.5f;
    int enemyCount = 0;
    int wave = 0;
    public int maxWaves = 6;
    public float waveDelay = 5;
    public int maxEnemies = 5;
    int inititalMaxEnemies;
    //public string wave2;

    Transform player;
    Vector3 playerPosition;
    public float playerTooClose = 0.8f;

    float spawnDistanceX;
    float spawnDistanceY;
    float spawnDistanceX2;
    float spawnDistanceY2;
    void Start()
    {
        /*Turns the the spawners transform position and makes a value that is 
        minus the spawnRange into a float that can be used in the new Vector2 later */
        spawnDistanceX = transform.position.x;
        spawnDistanceY = transform.position.y;
        spawnDistanceX2 = transform.position.x - spawnRangeX;
        spawnDistanceY2 = transform.position.y - spawnRangeY;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = new Vector3(Random.Range(spawnDistanceX2, spawnDistanceX), Random.Range(spawnDistanceY2, spawnDistanceY));
        inititalMaxEnemies = maxEnemies;
    }
    void Update()
    {
        resetAfterTimer += Time.deltaTime;
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        time3 += Time.deltaTime;

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //Checks the distance between the player and the current random spawnpoint
        Vector3 playerPosition = (player.position - spawnPosition);
        //Vector2 playerDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

        if (time > waveDelay && enemyCount < maxEnemies && wave < maxWaves && time2 > timeTillStart)
        {           
            //If the player to too close to the last randomly set spawnpoint it will keep getting a new random position until it can spawn
            if (playerPosition.magnitude > playerTooClose && time3 > spawnDelay)
            {
                resetAfterTimer = 0;
                time3 = 0;
                Instantiate(enemy, spawnPosition, Quaternion.identity);
                //Instantiate(spawnSound, spawnPosition, Quaternion.identity);
                enemyCount++;
                RandoomPosition();
            }
                else
                {
                RandoomPosition();
                }
        }
        if (resetAfterTimer > resetEvery && resetAfterTime == true && wave == maxWaves)
        {
            resetAfterTimer = 0;
            maxEnemies = inititalMaxEnemies;
            time = 0;
            time2 = 0;
            enemyCount = 0;
            wave = 0;
        }

        if(enemyCount == maxEnemies)
        {
            wave++;
            time = 0;
            enemyCount = 0;
            //maxEnemies += 1;
        }
    }
  
    void RandoomPosition() //Randomly selects a range in which to spawn the next Enemy
    {
        spawnPosition = new Vector3(Random.Range(spawnDistanceX2, spawnDistanceX), Random.Range(spawnDistanceY2, spawnDistanceY));
    }
    

}
