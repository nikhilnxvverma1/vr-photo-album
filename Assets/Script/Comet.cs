using UnityEngine;
using System.Collections;

//Author: Nikhil 
public class Comet : MonoBehaviour {

	public float domeRadius=3f;
	public GameObject cometPrefab;
	private GameObject currentComet;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool cometShootInProgress;
	private float totalTimeForCometShoot=3.0f;
	private float timePassedSinceLastShoot=0f;
	private float nextShootWaitPeriod=0f;


	// Use this for initialization
	void Start () {
		cometShootInProgress=false;
	}

	// Update is called once per frame
	void Update () {
		if(cometShootInProgress){
			timePassedSinceLastShoot+=Time.deltaTime;
			float t=timePassedSinceLastShoot/totalTimeForCometShoot;
			currentComet.transform.position = Vector3.Lerp(startPosition,endPosition,t);
			if(t>1){
				cometShootInProgress=false;
				Destroy(currentComet,5);
				currentComet=null;
				nextShootWaitPeriod=Random.Range(4.0f,10.0f);
			}
		}else{
			
			nextShootWaitPeriod-=Time.deltaTime;
			cometShootInProgress=nextShootWaitPeriod<=0;
//			cometShootInProgress=Random.Range(1,400)==55;//low chance that this will get set any frame
			if(cometShootInProgress){
				currentComet=Instantiate(cometPrefab);
				timePassedSinceLastShoot=0f;

				SetStartAndEndPositions();
			}
		}
	}

	private void SetStartAndEndPositions(){
		float level=0.75f;
		float y = domeRadius * level;	
		float t=Mathf.Atan(level);
		//		float t=Random.Range(0*Mathf.Deg2Rad,90*Mathf.Deg2Rad); //we only want them in the upper part

		//randomize at the top of the dome by controlling the angle that sets y 
		float s=Random.Range(0,2*Mathf.PI);
		float x = domeRadius * Mathf.Cos(s) * Mathf.Sin(t);
		float z = domeRadius * Mathf.Sin(s) * Mathf.Sin(t);
			
		startPosition= new Vector3(x,y,z);

		//end position should be exactly diagonally opposite (other end of the diameter of the circle)
		x = domeRadius * Mathf.Cos(s+Mathf.PI) * Mathf.Sin(t);
		z = domeRadius * Mathf.Sin(s+Mathf.PI) * Mathf.Sin(t);

		endPosition=new Vector3(x,y,z);
	}

}