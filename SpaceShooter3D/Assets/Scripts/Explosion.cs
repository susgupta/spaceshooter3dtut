using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {

    //reference for explosion
    [SerializeField]
    GameObject explosion;

    //reference for blow-up effect object
    [SerializeField]
    GameObject blowUp;

    //refernce for rigid body 
    [SerializeField]
    Rigidbody rigidBody;

    //reference for hit modifier
    [SerializeField]
    float laserHitModifier = 100.0f;

    //reference for shields
    [SerializeField]
    Shield shield;

    void OnCollisionEnter(Collision collision)
    {
        //find each contact points in collision, invoke hit
        foreach (ContactPoint contactPoint in collision.contacts)
        {
            Hit(contactPoint.point);
        }
    }

    //used for effects as well destroy
    public void BlowUp()
    {
        //send event for player death
        EventManager.PlayerDeath();   
        //simply instaniate
        Instantiate(blowUp, transform.position, Quaternion.identity);
        //destroy the attached gameobject
        Destroy(gameObject);
    }

    //method to call when hit something
    public void Hit(Vector3 position)
    {
        //create explosion than have it last for 6 seconds
        GameObject explosionObject = Instantiate(explosion, position, Quaternion.identity, transform) as GameObject;
        Destroy(explosionObject, 6f);

        //check for shields then invoke take damage
        if (shield == null)
        {
            return;
        }

        shield.TakeDamage();
    }

    //method to add force effects
    public void AddForce(Vector3 hitPosition, Transform hitSource)
    {
        Debug.Log("AddForce " + gameObject.name + " -> " + hitSource.name);
        //if not rigidBody - return out
        if (rigidBody == null)
        {
            return;
        }

        Vector3 forceVector = (hitSource.position - hitPosition).normalized;
        rigidBody.AddForceAtPosition(forceVector * laserHitModifier, hitPosition, ForceMode.Impulse);
    }
}
