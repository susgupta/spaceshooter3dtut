using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    //reference to all lasers
    [SerializeField]
    Laser[] lasers;

	void Update()
    {
        //for now space-bar is laser shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //fire all lasers
            foreach(Laser laser in lasers)
            {
                //get laser position
                //Vector3 laserPosition = transform.position + (transform.forward * laser.Distance());
                laser.FireLaser();
            }
        }
    }
}
