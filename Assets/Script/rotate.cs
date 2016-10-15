using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	public GameObject g;
	public float x, y, z;
	public float Acolor = 0.5f;
	// Use this for initialization
	void Start () {
		var rend = g.GetComponent<Renderer>();
		var color = rend.material.color;
		color.a = Acolor;
		rend.material.color = color;
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (x, y, z);	
		
	}
}
