using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Explosion))]
public class Asteroid : MonoBehaviour {

    //reference for scale values
    [SerializeField]
    float minScale = 0.8f;
    [SerializeField]
    float maxScale = 1.2f;

    //reference for rotation values
    [SerializeField]
    float rotationOffset = 100f;

    //want same value for asteroid delay destruction for EVERY instance
    public static float destructionDelay = 1.0f;

    //store reference of transform
    Transform currentTransform;

    //reference for rotation
    Vector3 randomRotation;
    
    void Awake()
    {
        currentTransform = transform;
    }

    void Start()
    {
        //random size
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);
        currentTransform.localScale = scale;

        //random rotation
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    void Update()
    {
        //apply random rotation
        currentTransform.Rotate(randomRotation * Time.deltaTime);
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
