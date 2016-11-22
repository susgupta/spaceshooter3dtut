using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    //reference for follow target
    [SerializeField]
    Transform target;

    //camera position, init with default values.
    [SerializeField]
    Vector3 defaultDistance = new Vector3(0f, 2f, -10f);

    [SerializeField]
    float distanceDamp = 10f;

    //speed of camera
    public Vector3 velocity = Vector3.one;

    //cache for target
    Transform currentTransform;

    void Awake()
    {
        currentTransform = transform;
    }

    void LateUpdate()
    {
        if (!FindTarget())
        {
            return;
        }

        SmoothFollow();        
    }

    void SmoothFollow()
    {
        Vector3 toPosition = target.position + (target.rotation * defaultDistance);
        Vector3 currentPosition = Vector3.SmoothDamp(currentTransform.position, toPosition, ref velocity, distanceDamp);
        currentTransform.position = currentPosition;

        currentTransform.LookAt(target, target.up);
    }

    bool FindTarget()
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
}
