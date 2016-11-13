using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickHandler : MonoBehaviour {
	public Button myButton;
	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(LoadOnClick);
	}
	
	void LoadOnClick()
	{
		SceneManager.LoadScene("Lobby");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
