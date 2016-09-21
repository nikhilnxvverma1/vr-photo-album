using UnityEngine;
using System.Collections;

public class Tilemap : MonoBehaviour {

	Tile [][] tileGrid;
	StreakPlacement [] streakPlacements;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

enum Direction{
	North,
	East,
	South,
	West
}