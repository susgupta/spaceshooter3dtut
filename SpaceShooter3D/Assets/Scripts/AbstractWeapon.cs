using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class that has common behaviour for weapon behaviour.
/// </summary>
public abstract class AbstractWeapon : MonoBehaviour {

    //distance of laser
    [SerializeField]
    protected float maxDistance = 300f;

    //create explosion by checking if has explosion
    protected void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        //get explosion, see if one is attached
        Explosion explosion = target.GetComponent<Explosion>();
        //if so carry out explosion
        if (explosion != null)
        {
            explosion.Hit(hitPosition);
            explosion.AddForce(hitPosition, transform);
        }
    }

    protected Vector3 CastRay()
    {
        //get colliders hit on cast
        RaycastHit hit;
        Vector3 forwardDirection = transform.TransformDirection(Vector3.forward) * maxDistance;

        //cast the ray
        if (Physics.Raycast(transform.position, forwardDirection, out hit))
        {
            //if hit return point of contact
            Debug.Log("We hit: " + hit.transform.name);
            Vector3 rayCastHitPosition = hit.point;
            SpawnExplosion(hit.point, hit.transform);

            return rayCastHitPosition;
        }

        Debug.Log("We missed.");
        return transform.position + (transform.forward * maxDistance);
    }

}
