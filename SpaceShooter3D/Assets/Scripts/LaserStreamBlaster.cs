using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BeamParam))]
public class LaserStreamBlaster : MonoBehaviour {

    //reference to constant laser stream weapon
    [SerializeField]
    GameObject laserStreamPrefab;

    //reference to laser wave effect
    [SerializeField]
    GameObject laserWavePreFab;

    //private reference to created laser stream
    private GameObject currentLaserStream;

    //method that will call laser stream
    public void FireLaserStream()
    {
        //create laser wave effect
        GameObject wave = (GameObject)Instantiate(laserWavePreFab, transform.position, transform.rotation);
        wave.transform.Rotate(Vector3.left, 90.0f);

        //create constant laser
        currentLaserStream = (GameObject)Instantiate(laserStreamPrefab, transform.position, transform.rotation);

        //use beam param package manipulation
        BeamParam beamParam = GetComponent<BeamParam>();

        if (currentLaserStream.GetComponent<BeamParam>().bGero)
        {
            currentLaserStream.transform.parent = transform;
        }

        //leverage beam parameter settings
        Vector3 beamScale = new Vector3(beamParam.Scale, beamParam.Scale, beamParam.Scale);
        currentLaserStream.transform.localScale = beamScale;
        currentLaserStream.GetComponent<BeamParam>().SetBeamParam(beamParam);
    }

    //method that will stop laser stream
    public void StopLaserStream()
    {
        if (currentLaserStream != null)
        {
            currentLaserStream.GetComponent<BeamParam>().bEnd = true;
        }
    }
}
