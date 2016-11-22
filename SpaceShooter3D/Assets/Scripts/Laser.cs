using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
public class Laser : MonoBehaviour {

    //reference to see laser
    [SerializeField]
    float laserOffTime = 0.45f;

    //distance of laser
    [SerializeField]
    float maxDistance = 300f;

    //fire delay
    [SerializeField]
    float fireDelay = 2f;

    //reference to light
    Light laserLight;
    //reference to line renderer
    LineRenderer lineRenderer;
    //reference to canFire
    bool canFire;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }
	    
    void Start()
    {
        //turn off renderer and lights
        lineRenderer.enabled = false;
        laserLight.enabled = false;
        canFire = true;
    }

    Vector3 CastRay()
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

    //create explosion by checking if has explosion
    void SpawnExplosion(Vector3 hitPosition, Transform target)
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

    //fire laser
    public void FireLaser()
    {
        Vector3 targetPosition = CastRay();
        //invoke overload
        FireLaser(targetPosition);
    }

    //fire laser that is overloaded to take in target position
    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if (canFire)
        {
            //spawn explostion if have target
            if (target != null)
            {
                Debug.Log("Hit target: " + target.name);
                SpawnExplosion(targetPosition, target);
            } 
            
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);
            //after position is set turn on
            lineRenderer.enabled = true;
            laserLight.enabled = true;
            canFire = false;

            //turn off laser
            Invoke("TurnOffLaser", laserOffTime);
            //allow fire using delay
            Invoke("CanFire", fireDelay);
        }
    }

    public float Distance()
    {
        //for now simply return max distance
        return maxDistance;        
    }

    void TurnOffLaser()
    {
        lineRenderer.enabled = false;
        laserLight.enabled = false;        
    }
    
    void CanFire()
    {
        canFire = true;
    }    
}
