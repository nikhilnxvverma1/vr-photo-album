using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Floor floor;
	public Prop prop;
	public Ceiling ceiling;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

enum TileLayer{
	FloorMap,
	PropMap,
	CeilingMap
}
