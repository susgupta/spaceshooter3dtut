using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//use for in radar
public class RadarObject {

    //the actual radar image associated with game object to be in radar
	public Image Icon
    {
        get;
        set;
    }

    //the owning game object to be shown in as radar object
    public GameObject Owner
    {
        get;
        set;
    }
}
