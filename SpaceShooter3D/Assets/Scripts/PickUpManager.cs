using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour {

    //reference for pickup prefab
    [SerializeField]
    GameObject pickUpPrefab;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += PlacePickUp;        
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= PlacePickUp;        
    }

    void PlacePickUp()
    {
        //create pickup at location
        Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
    }
}
