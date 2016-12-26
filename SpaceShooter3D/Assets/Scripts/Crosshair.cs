using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    //reference to texture sprite for crosshair
    [SerializeField]
    Texture2D crossHair;

    //adjust box size for cross-hair, height and width
    [SerializeField]
    float crossHairBoxHeight = 15.0f;
    [SerializeField]
    float crossHairBoxWidth = 15.0f;

    //adjustments for cross-hair location
    [SerializeField]
    float crossHairHorizontalPositonAdjusment = 45.0f;

    [SerializeField]
    float power = 10;
    
    void OnGUI()
    {
        //get current ray of the attached transform
        Ray currentRay = new Ray(transform.position, transform.forward);
        
        //calculate cross-hair position
        Vector3 crossHairPosition = Camera.main.WorldToScreenPoint(currentRay.GetPoint(power));
        crossHairPosition.y = Screen.height - crossHairPosition.y;
        crossHairPosition.x = crossHairHorizontalPositonAdjusment + crossHairPosition.x;
        
        //draw cross-hair box in front
        Rect crossHairBox = new Rect(crossHairPosition.x - (crossHair.width / 2), crossHairPosition.y - (crossHair.height / 2), crossHair.width, crossHair.height);
        crossHairBox.size = new Vector2(crossHairBoxHeight, crossHairBoxHeight);

        //paint on GUI canvas
        GUI.Label(crossHairBox, crossHair);
    }    
}
