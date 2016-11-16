using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {
	public float speed=20f;
	public float x;
	public float y;
	public float z;
	public float length;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		length = speed * Time.deltaTime;
		if (x > 20) {
			x = Random.Range(-20, 20);
			y = Random.Range(10, 17);
			z = Random.Range(-10, -7);
		}
		x += length;
		transform.position = new Vector3(x,y,z);
	}
}
