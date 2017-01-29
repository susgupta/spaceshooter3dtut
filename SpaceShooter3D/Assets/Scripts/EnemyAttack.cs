using UnityEngine;
using System.Collections;

public class EnemyAttack : AbstractEnemyBehaviour {

    //reference to laser
    [SerializeField]
    Laser laser;

    //hit position of target
    private Vector3 hitPosition;

    void Update()
    {
        //if no player target - return
        if (!FindTarget(GetTargetTagFromTargetState()))
        {
            return;
        }

        InFront();
        HasLineOfSightRaycast();
        if (InFront() && HasLineOfSightRaycast())
        {
            FireLasers();
        }
    }

    //check to see if target is in front
	private bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        //if angle is in 'forward' range
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            //Debug.DrawLine(transform.position, target.position, Color.green);
            return true;
        }

        //Debug.DrawLine(transform.position, target.position, Color.yellow);
        return false;
    }

    //see if enemy has line of sight
    private bool HasLineOfSightRaycast()
    {
        RaycastHit hit;

        Vector3 direction = target.position - transform.position;
        
        if (Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance()))
        {

            string targetTag = GetTargetTagFromTargetState();
            Debug.Log("Targeting: " + targetTag);

            //if hit the target
            if (hit.transform.CompareTag(targetTag))
            {
                Debug.Log("Have hit line of sight on: " + targetTag);
                hitPosition = hit.transform.position;
                return true;
            }
        }

        return false;
    }

    //fire enemy laser
    private void FireLasers()
    {
        Debug.Log("Fire Lasers!!!");
        laser.FireLaser(hitPosition, target);        
    }    
}
