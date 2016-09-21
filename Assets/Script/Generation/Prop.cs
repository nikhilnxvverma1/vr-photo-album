using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour {

	PropType type;
	Direction direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

enum PropType{
	Piano,
	Table,
	Chair,
	FlowerPot,
	Stool
	//etc, more as they are thought of
}