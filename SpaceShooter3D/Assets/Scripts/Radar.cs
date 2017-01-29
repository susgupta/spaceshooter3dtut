using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//behavior for the game radar
public class Radar : MonoBehaviour {

    //reference for player transform
    [SerializeField]
    Transform playerTransform;

    //reference for radar map scale, increase or decrease relative
    [SerializeField]
    float mapScale = 0.1f;

    //list of radar objects
    private static List<RadarObject> radarObjects = new List<RadarObject>();

    /// <summary>
    /// Method that allows game objects to register themselves with the radar
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="image"></param>
    public static void RegisterRadarObject(GameObject gameObject, Image image)
    {
        //simply create
        Image instaniatedImage = Instantiate(image);
        RadarObject radarObject = new RadarObject();
        radarObject.Icon = instaniatedImage;
        radarObject.Owner = gameObject;

        //add to list
        radarObjects.Add(radarObject);
    }
    
    /// <summary>
    /// Method that allows to remove from radar and destroy image on radar
    /// </summary>
    /// <param name="gameObject"></param>
    public static void RemoveRadarObject(GameObject gameObject)
    {
        List<RadarObject> newRadarList = new List<RadarObject>();

        //go thru current list of radar objects
        foreach (RadarObject currentRadarObject in radarObjects)
        {
            //if match from incoming object
            if (currentRadarObject.Owner == gameObject)
            {
                //remove the image
                Destroy(currentRadarObject.Icon);
                continue;
            }
            else
            {
                //add to new list - the current object
                newRadarList.Add(currentRadarObject);
            }
        }

        //update static list
        radarObjects.RemoveRange(0, radarObjects.Count);
        radarObjects.AddRange(newRadarList);
    }

    //actually draw the radar for each registered object
    void DrawRadarDots()
    {
        foreach(RadarObject radarObject in radarObjects)
        {
            Vector3 radarPosition = radarObject.Owner.transform.position - playerTransform.position;
            float distanceToObject = Vector3.Distance(playerTransform.position, radarObject.Owner.transform.position) * mapScale;

            //work out the angle between the player
            float deltaY = Mathf.Atan2(radarPosition.x, radarPosition.z) * Mathf.Rad2Deg - 270 - playerTransform.eulerAngles.y;

            //calculate position based on circle - the 'radar', polar equation
            radarPosition.x = distanceToObject * Mathf.Cos(deltaY * Mathf.Deg2Rad) * -1;
            radarPosition.z = distanceToObject * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            radarObject.Icon.transform.SetParent(this.transform);
            radarObject.Icon.transform.position = new Vector3(radarPosition.x, radarPosition.z, 0) + transform.position;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        DrawRadarDots();
    }
}
