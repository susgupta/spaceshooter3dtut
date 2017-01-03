using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    //reference for enemy prefab
    [SerializeField]
    GameObject enemyPrefab;

    //reference for enemy entrance effect
    [SerializeField]
    GameObject enemyEntranceEffectPrefab;

    //reference for enemy entrance effect duration
    [SerializeField]
    float enemyEntranceEffectDuration = 1.0f;

    //reference for spawn timer
    [SerializeField]
    float spawnTimer = 5f;

    //for now have max spawner so doesn't go crazy with enemies
    [SerializeField]
    int maxNumberSpawnedEnemies = 6;

    //modifier for spawn distance
    [SerializeField]
    float spawnDistance = 1;
    
    int currentNumberSpawnedEnemies = 0;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

    void OnDisable()
    {
        //remove delegate to unsubscribe
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
            //default to attached object position
            Vector3 positionToUse = transform.position;

            //only do this if a player is in scene
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                //use camera position so player can 'see spawning'
                positionToUse = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
            }

            //create effect for enemy entrance - as 'shock warp in'
            GameObject enemyEntranceEffect = Instantiate(enemyEntranceEffectPrefab, positionToUse, Quaternion.identity) as GameObject;
            //destroy
            Destroy(enemyEntranceEffect, enemyEntranceEffectDuration);
            //yield to wait
            new WaitForSeconds(enemyEntranceEffectDuration);

            //now create enemy
            Instantiate(enemyPrefab, positionToUse, Quaternion.identity);
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
