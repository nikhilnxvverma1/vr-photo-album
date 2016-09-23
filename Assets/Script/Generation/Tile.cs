using UnityEngine;

public class Tile {

	public Floor floor;
	public Prop prop;
	public Ceiling ceiling;

}

enum TileLayer{
	FloorMap,
	PropMap,
	CeilingMap
}
