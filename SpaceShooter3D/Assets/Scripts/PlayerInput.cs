using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    //reference to all lasers
    [SerializeField]
    Laser[] lasers;

    //reference to all shot spawns
    [SerializeField]
    LaserBulletBlaster[] laserBulletBlasters;

    //reference to laser stream - for now only want one
    [SerializeField]
    LaserStreamBlaster laserStreamBlaster;
    
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
                laserStreamBlaster.StopLaserStream();
            }
        }
        
        //for now space-bar is shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggleToLasers)
            {
                laserStreamBlaster.FireLaserStream();                
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

        //if its space up
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (toggleToLasers)
            {
                laserStreamBlaster.StopLaserStream();
            }

        }
    }
}
