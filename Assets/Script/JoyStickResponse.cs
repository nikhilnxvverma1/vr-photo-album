using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class JoyStickResponse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace) || 
			(DataScan.OS == DataScan.OS_TYPE.WINDOWS && Input.GetKeyDown ("joystick button 6")) ||
			(DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown ("joystick button 10"))){
			Debug.Log ("Back Button detected Taking to Tutorial Scene");
			DataScan.currentAlbum = DataScan.rootModel.albumList [0];
			SceneManager.LoadScene ("Tutorial");
		} 		
	}
}
