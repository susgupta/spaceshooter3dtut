using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour {

    //reference for pickup prefab
    [SerializeField]
    GameObject pickUpPrefab;

    //modifier for spawn distance
    [SerializeField]
    float spawnDistance = 1;

    //reference for spawn timer
    [SerializeField]
    float spawnTimer = 5f;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += StartSpawningPickUps;
        EventManager.onPlayerDeath += StopSpawningPickUps;
    }

    void OnDisable()
    {
        //remove delegate to unsubscribe
        EventManager.onStartGame -= StartSpawningPickUps;
        EventManager.onPlayerDeath += StopSpawningPickUps;
    }

    //method to start spawning
    void StartSpawningPickUps()
    {
        InvokeRepeating("PlacePickUp", spawnTimer, spawnTimer);
    }

    //method to stop spawning
    void StopSpawningPickUps()
    {
        CancelInvoke("PlacePickUp");
    }

    void PlacePickUp()
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

        //create pickup at location
        Instantiate(pickUpPrefab, positionToUse, Quaternion.identity);
    }
}
