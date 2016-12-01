using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	public bool isEnabled;
	static bool AudioBegin = false;
	private static System.Random rng = new System.Random();
	int index = 0;
	private static AudioSource audioSource;

	void Start () {
		Shuffle ();
		if(audioSource == null)
			audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying)
		{
			var c = Resources.Load(DataScan.musicList[index], typeof(AudioClip)) as AudioClip;
			audioSource.clip = c;
			if (isEnabled) {
				audioSource.Play ();
				Debug.Log (c.length);
				Debug.Log(" Played "+DataScan.musicList[index]);
			}
			DontDestroyOnLoad(this.gameObject);
			AudioBegin = true;
			index = (index + 1) % DataScan.musicList.Count;
		}
	}

	public static void Shuffle()  
	{  
		int n = DataScan.musicList.Count;  
		while (n >0) {  
			n--;  
			int k = rng.Next(n + 1);  
			var value = DataScan.musicList[k];  
			DataScan.musicList[k] = DataScan.musicList[n];  
			DataScan.musicList[n] = value;

		}  
	}
}
