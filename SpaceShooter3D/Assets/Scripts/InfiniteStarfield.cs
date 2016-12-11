using UnityEngine;
using System.Collections;

public class InfiniteStarfield : MonoBehaviour {

    //reference for transform, for performance
    Transform currentTransform;

    //reference to particle
    private ParticleSystem.Particle[] points;

    //set for max stars
    [SerializeField]
    int starsMax = 100;
    //star size
    [SerializeField]
    float starSize = 1.0f;
    //star distance spacing
    [SerializeField]
    float starDistance = 10.0f;
    //star clip distance
    [SerializeField]
    float starClipDistance = 1.0f;

    private float starDistanceSquare;
    private float starClipDistanceSquare;

    // Use this for initialization
    void Start ()
    {
        currentTransform = transform;
        starDistanceSquare = starDistance * starDistance;
        starClipDistanceSquare = starClipDistance * starClipDistance;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (points == null)
        {
            CreateStars();
        }

        for (int i = 0; i < starsMax; i++)
        {
            //if go outside range, reset back to it.
            if ((points[i].position - currentTransform.position).sqrMagnitude >starDistanceSquare)
            {
                points[i].position = Random.insideUnitSphere.normalized * starDistance + transform.position;
            }

            //if in clip range
            if ((points[i].position - currentTransform.position).sqrMagnitude <= starClipDistanceSquare)
            {
                //get percentage of clip
                float percentage = (points[i].position - currentTransform.position).sqrMagnitude / starClipDistanceSquare;
                
                //set the alpha and sizebased on clip percentage
                points[i].color = new Color(1, 1, 1, percentage);
                points[i].size = percentage * starSize;
            }
        }

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.SetParticles(points, points.Length);
    }

    //create starfield stars
    private void CreateStars()
    {
        points = new ParticleSystem.Particle[starsMax];

        //set star properties
        for (int i = 0; i < starsMax; i++)
        {
            points[i].position = Random.insideUnitSphere * starDistance + transform.position;
            points[i].color = new Color(1, 1, 1, 1);
            points[i].size = starSize;
        }
    }
}
