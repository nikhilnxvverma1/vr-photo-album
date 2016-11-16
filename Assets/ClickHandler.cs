using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickHandler : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	void LoadOnClick()
	{
		SceneManager.LoadScene("Lobby");
	}
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Lobby");
        }
        else if (DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown("joystick button 16"))
        {
            SceneManager.LoadScene("Lobby");
        }
        else if (Input.GetKeyDown("joystick button 0"))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
