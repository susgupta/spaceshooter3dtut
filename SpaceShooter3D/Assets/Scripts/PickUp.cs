using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider))]
public class PickUp : AbstractSpaceObject {

    //value of score for pickUp
    [SerializeField]
    int pointValue = 100;

    void Start()
    {
        CreateRandomRotation();
    }

    void OnEnable()
    {
        //subscribe to event
        EventManager.onPlayerDeath += SelfDestruct;        
    }

    void OnDisable()
    {
        //un-subscribe to event
        EventManager.onPlayerDeath -= SelfDestruct;
    }

    //self destruct on enemy
    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        
        //see if collider is player
        if (collider.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit us!!");
            PickUpHit();
        }
    }

    //method to for hit actions
    public void PickUpHit()
    {
        //call event need to add to score
        EventManager.ScorePoints(pointValue);

        //destroy self
        Destroy(gameObject);
    }
}
