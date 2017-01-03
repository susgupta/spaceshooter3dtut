using UnityEngine;
using System.Collections;

public class LaserBulletBlaster : AbstractWeapon {

    //reference for bullet prefab
    [SerializeField]
    GameObject laserBulletPrefab;

    //reference for shot effect prefab
    [SerializeField]
    GameObject bulletShotEffectPrefab;

    //control bullet speed
    [SerializeField]
    int bulletSpeed = 80;

    //reference for shot delay
    [SerializeField]
    float laserBulletDelay = 0.2f;

    //reference for shot effect duration
    [SerializeField]
    float shotEffectDuration = 0.1f;

    //cooldown indicator
    private bool isCoolDown;
    
    //method to shoot bullet
    public void ShootBullet()
    {
        //generate shot effect
        GameObject bulletShotEffect = Instantiate(bulletShotEffectPrefab, transform.position, Quaternion.identity) as GameObject;
        bulletShotEffect.transform.parent = transform;
        //destroy after time
        Destroy(bulletShotEffect, shotEffectDuration);

        //instantiate bullet from prefab
        GameObject laserBullet = Instantiate(laserBulletPrefab, transform.position, transform.rotation) as GameObject;
        //add force to bullet to propel it
        laserBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        
        //set cooldown and delay coroutine
        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    //cooldown routine, to be used in coroutine
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, laserBulletDelay));
    }
}
