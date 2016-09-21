using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	FloorType type;
	Direction direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
	
enum FloorType{
	Ground,
	Door,
	Window,
	Corner,
	//etc, more as they are thought of
}