using UnityEngine;
using System.Collections;

public class GeroBeamHit : MonoBehaviour {
	private GameObject ParticleA;
	private GameObject ParticleB;
	private GameObject HitFlash;
	
	private float PatA_rate;
	private float PatB_rate;

	private ParticleSystem PatA;
	private ParticleSystem PatB;
    public Color col;

	public void SetViewPat(bool bHitNow)
	{
		if(bHitNow){
			PatA.emissionRate = PatA_rate;
			PatB.emissionRate = PatB_rate;
			HitFlash.GetComponent<Renderer>().enabled = true;
		}else{
			PatA.emissionRate = 0;
			PatB.emissionRate = 0;
			HitFlash.GetComponent<Renderer>().enabled = false;
		}
	}

    //custom method added to destroy, invoke explosion if detected hit
    public void DestroyOnHit(GameObject hitObject)
    {
        if (hitObject != null)
        {
            Debug.Log("Laser Stream Beam hit: " + hitObject.tag);
            
            //make sure tag is not player
            if (hitObject.tag != "Player")
            {
                //get explosion component and check if it exists
                Explosion targetExplosion = hitObject.GetComponent<Explosion>();

                //if so invoke the target blow-up routine -TODO handle potential target shields
                if (targetExplosion != null)
                {
                    targetExplosion.BlowUp();
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        col = new Color(1, 1, 1);
		ParticleA = transform.FindChild("GeroParticleA").gameObject;
		ParticleB = transform.FindChild("GeroParticleB").gameObject;
		HitFlash = transform.FindChild("BeamFlash").gameObject;
		PatA = ParticleA.gameObject.GetComponent<ParticleSystem>();
		PatA_rate = PatA.emissionRate;
		PatA.emissionRate = 0;
		PatB = ParticleB.gameObject.GetComponent<ParticleSystem>();
		PatB_rate = PatB.emissionRate;
		PatB.emissionRate = 0;

		HitFlash.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        PatA.startColor = col;
        PatB.startColor = col;
        HitFlash.GetComponent<Renderer>().material.SetColor("_Color", col*1.5f);
    }
}
