using UnityEngine;

public class Tile {

	public Floor floor;
	public Prop prop;
	public Ceiling ceiling;

	public Tile(){
	}

	public Tile(Floor floor,Prop prop, Ceiling ceiling){
		this.floor=floor;
		this.prop=prop;
		this.ceiling=ceiling;
	}

	public string ToString(){
		return floor.ToString()+"-"+ceiling.ToString();
	}
}

enum TileLayer{
	FloorMap,
	PropMap,
	CeilingMap
}
