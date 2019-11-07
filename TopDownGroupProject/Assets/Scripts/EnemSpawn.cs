using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemSpawn : MonoBehaviour
{
    public float spawnTime = 5f;
    public float enemyCount = 5f;
    public float spawnDelay = 3f;
   
    public GameObject[] enemy;
    public Vector3 enposition;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
        
        
    }
    void Spawn()
    {
        //Instantiate a random enemy
        int enemyIndex = Random.Range(0, enemy.Length);
        Instantiate(enemy[enemyIndex], enposition, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
