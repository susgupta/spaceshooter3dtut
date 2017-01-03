using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour {

    [SerializeField]float maxBulletLifeTime = 2.5f;

	void Start()
    {
        //ensure bullet is destroyed after some max time
        Destroy(gameObject, maxBulletLifeTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Laser Bullet hit: " + collider.tag);

        //make sure tag is not player
        if (collider.tag != "Player")
        {
            //get explosion component and check if it exists
            Explosion targetExplosion = collider.GetComponent<Explosion>();

            //if so invoke the target blow-up routine -TODO handle potential target shields
            if (targetExplosion != null)
            {
                targetExplosion.BlowUp();
            }

            //always destroy bullet
            Destroy(gameObject);
        }        
    }
}
