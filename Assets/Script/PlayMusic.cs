using UnityEngine;
using System.Collections;
using UnityEditor;


public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	public bool isEnabled;
	void Start () {
		Debug.Log("Trying to play music: " + DataScan.currentAlbum.audio);
		AudioSource audio = GetComponent<AudioSource>();
		var c= AssetDatabase.LoadAssetAtPath (DataScan.currentAlbum.audio, typeof(AudioClip)) as AudioClip;
		if (c == null) {
			Debug.Log("Trying to play music: " + DataScan.currentAlbum.audio);

		}
		audio.clip = c;
		if(isEnabled) audio.Play();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
