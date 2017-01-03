using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Light))]
public class Thruster : MonoBehaviour {

    //reference to light
    Light thrusterLight;

    void Awake()
    {
        thrusterLight = GetComponent<Light>();
    }

    void Start()
    {
        //ensure lights, trail etc are disabled
        thrusterLight.intensity = 0;
    }       

    //method to control intensity
    public void Intensity(float intent)
    {
        //use constant to ensure to ramp up or down based on one
        thrusterLight.intensity = intent * 2f;
    }
}
