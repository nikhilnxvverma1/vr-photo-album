using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

	public float angle =0;
	public float angle2 = 0;
	public float angle3 = 0;
	public float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
	public float speed1 = 0;
	public float radius=17;
	public float radius1 = 1;
	public float x, y, z,x1,y1,z1;

	// Use this for initialization
	void Start () {		
	}

	// Update is called once per frame
	void Update () {
		angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=
		x = Mathf.Cos(angle2)*Mathf.Cos(angle)*radius;
		//z = Mathf.Sin(angle2)*radius;
		z = Mathf.Sin(angle)*radius;
		y = radius * Mathf.Sin(angle2)*Mathf.Cos(angle);
		angle3 += speed1*Time.deltaTime;
		x1 = x + Mathf.Cos (angle3) * radius1;
		y1 = y + Mathf.Sin (angle3) * radius1;
		transform.position = new Vector3(x1,y1,z);
	}
}


