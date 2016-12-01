using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class JoyStickResponse : MonoBehaviour {

	public GameObject menu;
	// Use this for initialization
	void Start () {
		if (menu != null) {
			Instantiate (menu);
			menu.SetActive (false);
		}

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
		if((DataScan.OS == DataScan.OS_TYPE.WINDOWS && Input.GetKeyDown ("joystick button 1")) ||
			(DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown ("joystick button 17"))){
			if (menu!=null) {
				Debug.Log ("Opening Menu");
				menu.gameObject.SetActive (true);
			}
		}
	}
}
