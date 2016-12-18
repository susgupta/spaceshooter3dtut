using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    //reference for enemy prefab
    [SerializeField]
    GameObject enemyPrefab;

    //reference for spawn timer
    [SerializeField] float spawnTimer = 5f;

    //for now have max spawner so doesn't go crazy with enemies
    [SerializeField]
    int maxNumberSpawnedEnemies = 6;

    int currentNumberSpawnedEnemies = 0;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= StartSpawning;
        EventManager.onPlayerDeath -= StopSpawning;
    }

    //method to spawn enemy
    void SpawnEnemy()
    {
        if (currentNumberSpawnedEnemies >= maxNumberSpawnedEnemies)
        {
            StopSpawning();
        }
        else
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentNumberSpawnedEnemies++;
        }        
    }

    //method to start spawning
    void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
    }

    //method to stop spawning
    void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
        //reset number of enemies
        currentNumberSpawnedEnemies = 0;
    }
}
