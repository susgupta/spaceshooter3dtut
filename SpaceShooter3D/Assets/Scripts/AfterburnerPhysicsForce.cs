using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class AfterburnerPhysicsForce : MonoBehaviour {

    //set properties of after burner particles
    [SerializeField]
    float effectAngle = 15.0f;
    [SerializeField]
    float effectWidth = 1.0f;
    [SerializeField]
    float effectDistance = 10.0f;

    //set force of after burner
    [SerializeField]
    float force = 10.0f;

    //referece to sphere collider
    private SphereCollider currentSphereCollider;
    //reference to colliders
    private Collider[] colliders;

    void OnEnable()
    {
        //set sphere reference
        currentSphereCollider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        //get with all colliders touching or inside the sphere
        colliders = Physics.OverlapSphere(transform.position + currentSphereCollider.center, currentSphereCollider.radius);

        for (int n = 0; n < colliders.Length; ++n)
        {
            if (colliders[n].attachedRigidbody != null)
            {
                //get inverse position of local collider
                Vector3 localPos = transform.InverseTransformPoint(colliders[n].transform.position);
                localPos = Vector3.MoveTowards(localPos, new Vector3(0, 0, localPos.z), effectWidth * 0.5f);

                //get inverse angles
                float angle = Mathf.Abs(Mathf.Atan2(localPos.x, localPos.z) * Mathf.Rad2Deg);
                float falloff = Mathf.InverseLerp(effectDistance, 0, localPos.magnitude);
                falloff *= Mathf.InverseLerp(effectAngle, 0, angle);

                //ack: Unity standard base package scripts
                Vector3 delta = colliders[n].transform.position - transform.position;
                colliders[n].attachedRigidbody.AddForceAtPosition(delta.normalized * force * falloff,
                                                             Vector3.Lerp(colliders[n].transform.position,
                                                                          transform.TransformPoint(0, 0, localPos.z),
                                                                          0.1f));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //check for editor time simulation to avoid null ref
        if (currentSphereCollider == null)
        {
            currentSphereCollider = GetComponent<SphereCollider>();
        }

        //adjust collider distance to form trail
        currentSphereCollider.radius = effectDistance * .5f;
        currentSphereCollider.center = new Vector3(0, 0, effectDistance * .5f);

        //get directions
        Vector3[] directions = new Vector3[] { Vector3.up, -Vector3.up, Vector3.right, -Vector3.right };
        var perpDirections = new Vector3[] { -Vector3.right, Vector3.right, Vector3.up, -Vector3.up };

        //render
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        for (int n = 0; n < 4; ++n)
        {
            Vector3 origin = transform.position + transform.rotation * directions[n] * effectWidth * 0.5f;

            Vector3 direction =
                transform.TransformDirection(Quaternion.AngleAxis(effectAngle, perpDirections[n]) * Vector3.forward);

            Gizmos.DrawLine(origin, origin + direction * currentSphereCollider.radius * 2);
        }
    }

}
