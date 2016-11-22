using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    //reference for scale values
    [SerializeField]
    float minScale = 0.8f;
    [SerializeField]
    float maxScale = 1.2f;

    //reference for rotation values
    [SerializeField]
    float rotationOffset = 100f;
    
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
}
