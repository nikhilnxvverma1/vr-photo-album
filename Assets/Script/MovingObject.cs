using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {
	public float angle =0;
	public float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
	public float radius=17;
	public float x, y, z;
	// Use this for initialization
	void Start () {		
	}

	// Update is called once per frame
	void Update () {
		
		angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=
		//TODO this is 2d rotation, For 3d rotation you also have to consider z
		x = Mathf.Cos(angle)*radius;
		y = Mathf.Sin(angle)*radius;
		transform.position = new Vector3(x,y,z);
	}
}
