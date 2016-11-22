using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {

    //movement speed
    [SerializeField]
    float movementSpeed = 50;

    //turn speed
    [SerializeField]
    float turnSpeed = 60f;

    //reference to thruster
    [SerializeField]
    Thruster[] thruster;

    //store reference of transform
    Transform currentTransform;

    void Awake()
    {
        currentTransform = transform;
    }

	void Update()
    {
        Turn();
        Thrust();
    }

    //turn function
    void Turn()
    {
        //get yaw or y-axis rotation
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        //get pitch or x-axis rotation
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        //got roll or z-axis rotation
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");

        //apply to transform
        currentTransform.Rotate(-pitch, yaw, -roll);
    }

    //thrust forward function
    void Thrust()
    {
        //only move player 'forward'
        if (Input.GetAxis("Vertical") > 0)
        {
            currentTransform.position += currentTransform.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            //for each thruster adjust intensity based on input
            foreach (Thruster currentThruster in thruster)
            {
                currentThruster.Intensity(Input.GetAxis("Vertical"));
            }
        }
        
        //turn on thruster effects
        //if (Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    ProcessThrusters(true);
        //}
        //else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    ProcessThrusters(false);
        //}        
    }

    //common player processing thrusters
    //private void ProcessThrusters(bool activate)
    //{
    //    foreach (Thruster currentThruster in thruster)
    //    {
    //        currentThruster.Activate(activate);
    //    }
    //}
}
