using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	public GameObject g;
	public float x, y, z;
	public float Acolor = 0.5f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (x, y, z);	
		Renderer[] rs = g.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in rs)
		{
			Material m = r.material;
			//m.color = new Color(0,0,.10f);
			r.material = m;
		}
	}
}
