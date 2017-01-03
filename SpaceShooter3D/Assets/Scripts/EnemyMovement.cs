using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(TrailRenderer))]
public class EnemyMovement : MonoBehaviour {

    //reference to target
    [SerializeField]
    Transform target;

    //reference to rotational damp
    [SerializeField]
    float rotationalDamp = 0.5f;

    //reference to rotational damp
    [SerializeField]
    float movementSpeed = 10.0f;

    //reference to offset for movement raycast
    [SerializeField]
    float rayCastOffset = 2.5f;

    //reference to detection distance
    [SerializeField]
    float detectionDistance = 20f;

    void OnEnable()
    {
        //subscribe to event
        EventManager.onPlayerDeath += FindMainCamera;
        EventManager.onStartGame += SelfDestruct;
    }

    void OnDisable()
    {
        //un-subscribe to event
        EventManager.onPlayerDeath -= FindMainCamera;
        EventManager.onStartGame -= SelfDestruct;
    }

    void Update()
    {
        //if no player target - return
        if (!FindPlayer())
        {
            return;
        }

        PathFinding();
        Move();
    }

    void Turn()
    {
        Vector3 turnPostion = target.position - transform.position;
        Quaternion turnRotation = Quaternion.LookRotation(turnPostion);
        //apply rotation, but gradually
        transform.rotation = Quaternion.Slerp(transform.rotation, turnRotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 rayCastOffsetVector = Vector3.zero;
        
        //get all offset vectors
        Vector3 left = transform.position - (transform.right * rayCastOffset);
        Vector3 right = transform.position + (transform.right * rayCastOffset);
        Vector3 up = transform.position + (transform.up * rayCastOffset);
        Vector3 down = transform.position - (transform.up * rayCastOffset);

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        //check right-left
        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            rayCastOffsetVector += Vector3.right;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            rayCastOffsetVector -= Vector3.right;
        }

        //check up-down
        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            rayCastOffsetVector -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            rayCastOffsetVector += Vector3.up;
        }

        //check if move vector is not zero, basically has some change from raycast
        if (rayCastOffsetVector != Vector3.zero)
        {
            transform.Rotate(rayCastOffsetVector * 15f * Time.deltaTime);
        }
        else
        {
            //call regular turn 
            Turn();
        }
    }

    bool FindPlayer()
    {
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            //now check if player is not destroyed
            if (playerObject != null)
            {
                target = playerObject.transform;
            }            
        }

        if (target == null)
        {
            return false;
        }

        return true;
    }

    //method for enemy to find main camera
    void FindMainCamera()
    {
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        target = mainCameraObject.transform;
    }

    //self destruct on enemy
    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
