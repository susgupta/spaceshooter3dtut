using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider))]
public class PickUp : AbstractSpaceObject {
    
    void Start()
    {
        CreateRandomRotation();
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

        //destroy self
    }
}
