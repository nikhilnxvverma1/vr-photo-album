using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float speed=10f;

	// Use this for initialization
	void Start () {
		//lock the cursor's position
		Cursor.lockState=CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		float translation=Input.GetAxis("Vertical") * speed;
		float strafe=Input.GetAxis("Horizontal") *speed;
		translation *=Time.deltaTime; 
		strafe *=Time.deltaTime; 

		transform.Translate(strafe,0,translation);

		//resume moving the cursor when user presses escape
		if(Input.GetKeyDown("escape")){
			Cursor.lockState=CursorLockMode.None;
		}
	
	}
}
