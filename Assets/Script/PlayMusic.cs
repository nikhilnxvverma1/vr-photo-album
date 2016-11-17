using UnityEngine;
using System.Collections;


public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	public bool isEnabled;
	static bool AudioBegin = false;
	void Start () {
		if (!AudioBegin)
		{
			AudioSource audio = GetComponent<AudioSource>();
			var c = Resources.Load("LobbyMusic", typeof(AudioClip)) as AudioClip;
			audio.clip = c;
			if (isEnabled) audio.Play();
			DontDestroyOnLoad(this.gameObject);
			AudioBegin = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
