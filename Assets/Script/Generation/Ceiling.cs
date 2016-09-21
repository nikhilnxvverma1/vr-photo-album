using UnityEngine;
using System.Collections;

public class Ceiling : MonoBehaviour {

	CeilingType type;
	Direction direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}

enum CeilingType{
	Chandelier,
	Vent,
	Fan,
	FireAlarm,
	HangingBanner
	//etc, more as they are thought of
}