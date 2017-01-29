using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeRadarObject : MonoBehaviour {

    //reference to image
    [SerializeField]
    Image image;

	// Use this for initialization
	void Start ()
    {
        //simply invoke to register with radar
        Radar.RegisterRadarObject(gameObject, image);
	}
	
	// Called when object is destroyed
	void OnDestroy ()
    {
        //remove from radar
        Radar.RemoveRadarObject(gameObject);
	}
}
