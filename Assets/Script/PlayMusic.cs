using UnityEngine;
using System.Collections;
using UnityEditor;


public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	public bool isEnabled;
	static bool AudioBegin = false;
	void Start () {
		if (!AudioBegin)
		{
			Debug.Log("Trying to play music: " + DataScan.currentAlbum.audio);
			AudioSource audio = GetComponent<AudioSource>();
			var c = AssetDatabase.LoadAssetAtPath(DataScan.currentAlbum.audio, typeof(AudioClip)) as AudioClip;
			if (c == null)
			{
				Debug.Log("Trying to play music: " + DataScan.currentAlbum.audio);

			}
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
