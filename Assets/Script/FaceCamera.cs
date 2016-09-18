using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
	GameObject picture;
	// Use this for initialization
	void Start () {
		picture = this.transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		picture.transform.rotation = Camera.main.transform.rotation;
	}
}
