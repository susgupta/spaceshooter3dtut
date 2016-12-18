using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Explosion))]
public class Asteroid : AbstractSpaceObject{

    //reference for scale values
    [SerializeField]
    float minScale = 0.8f;
    [SerializeField]
    float maxScale = 1.2f;
    
    //want same value for asteroid delay destruction for EVERY instance
    public static float destructionDelay = 1.0f;
    
    void Start()
    {
        //random size
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);
        currentTransform.localScale = scale;

        //random rotation
        CreateRandomRotation();
    }
    
    //self-destruct on the asteroid
    public void SelfDestruct()
    {
        //create timer for destruction effect
        float timer = Random.Range(0, destructionDelay);
        //invoke boom with timer value
        Invoke("GoBoom", timer);
    }

    //call explosion effect
    public void GoBoom()
    {
        //call explosion
        Explosion explosion = GetComponent<Explosion>();
        explosion.BlowUp();
    }
}
