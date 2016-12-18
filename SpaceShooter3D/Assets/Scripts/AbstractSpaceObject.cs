using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class that has common behaviour for space objects like asteroids and pickUps
/// </summary>
public abstract class AbstractSpaceObject : MonoBehaviour {

    //reference for rotation values
    [SerializeField]
    protected float rotationOffset = 100f;

    //reference for rotation
    protected Vector3 randomRotation;

    //store reference of transform
    protected Transform currentTransform;

    //common method to create space object with random rotation
    protected void CreateRandomRotation()
    {
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }

    void Awake()
    {
        currentTransform = transform;
    }
    
	// Update is called once per frame
	void Update ()
    {
        //apply random rotation
        currentTransform.Rotate(randomRotation * Time.deltaTime);
    }
}
