using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    //reference to all lasers
    [SerializeField]
    Laser[] lasers;

    //reference to all shot spawns
    [SerializeField]
    LaserBulletBlaster[] laserBulletBlasters;

    //toggle indicator for weapon to lasers
    private bool toggleToLasers = false;

	void Update()
    {
        //for now use 't' as toggle between lasers and bullets
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (toggleToLasers == false)
            {
                toggleToLasers = true;
            }
            else if (toggleToLasers == true)
            {
                toggleToLasers = false;
            }
        }
        
        //for now space-bar is shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggleToLasers)
            {
                //fire all lasers
                foreach (Laser laser in lasers)
                {
                    laser.FireLaser();
                }
            }
            else
            {
                //fire laser bullets
                foreach (LaserBulletBlaster laserBulletBlaster in laserBulletBlasters)
                {
                    laserBulletBlaster.ShootBullet();
                }
            }
        }
    }
}
