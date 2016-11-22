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

    ////method to do various trail renderer effects
    //public void Activate(bool activtate = true)
    //{
    //    if (activtate)
    //    {
    //        trailRenderer.enabled = true;
    //        thrusterLight.enabled = true;
    //        //turn on particle effects
    //        //turn on sound
    //        //etc.
    //    }
    //    else
    //    {
    //        trailRenderer.enabled = false;
    //        thrusterLight.enabled = false;
    //        //turn off particle effects
    //        //turn off sound
    //        //etc.
    //    }
    //}

    //method to control intensity
    public void Intensity(float intent)
    {
        //use constant to ensure to ramp up or down based on one
        thrusterLight.intensity = intent * 2f;
    }
}
