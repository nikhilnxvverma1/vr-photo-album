using UnityEngine;
using System.Collections;

public class CreateTwinklingStars : MonoBehaviour {

	public GameObject starPrefab;
	public float domeRadius;
	public int howMany;

	// Use this for initialization
	void Start () {
		GenerateStars(howMany);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateStars(int n){
		for(int i=0;i<n;i++){
			GameObject star=Instantiate(starPrefab);
			float s=Random.Range(0,2*Mathf.PI);
			float t=Random.Range(0,2*Mathf.PI);
			float x = domeRadius * Mathf.Cos(s) * Mathf.Sin(t);
			float z = domeRadius * Mathf.Sin(s) * Mathf.Sin(t);
			float y = domeRadius * Mathf.Cos(t);
			star.transform.position=new Vector3(x,y,z);
		}
	}
}
