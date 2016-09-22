using UnityEngine;

public class Tilemap : MonoBehaviour {

	int rows,columns;	

	Tile [][] tileGrid;
	StreakPlacement [] streakPlacements;

	// Use this for initialization
	void Start () {
		ReadTileMapFile("tilemap.csv");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ReadTileMapFile(string path){
		//read the contents of a CSV file
		string csv=System.IO.File.ReadAllText(path);//must be in the same folder

		//split each new line
		string [] lines=csv.Split("\n"[0]);

		//first line contains the size of the grid
		string []dimensions=lines[0].Split(',');
		rows=int.Parse(dimensions[0]);
		columns=int.Parse(dimensions[1]);

		tileGrid=new Tile[rows][];

		//instantiate the row of tiles first
		for(int i=0;i<rows;i++){
			tileGrid[i]=new Tile[columns];	
		}


		TileLayer currentTileLayer=TileLayer.FloorMap;//default
		int currentMapRow=0;

		for (int i=1;i<lines.Length;i++){
						
			string []tiles=lines[i].Split(',');

			//beginning of each grid contains the type: floormap, ceilingmap, propmap
			if(string.Equals(tiles[0],"FloorMap")){
				currentTileLayer=TileLayer.FloorMap;
				currentMapRow=0;
			}else if(string.Equals(tiles[0],"PropMap")){
				currentTileLayer=TileLayer.PropMap;
				currentMapRow=0;
			}else if(string.Equals(tiles[0],"CeilingMap")){
				currentTileLayer=TileLayer.CeilingMap;
				currentMapRow=0;
			}else{
				//this means its a continuation of the currentTileLayer
				for(int j=0;j<columns;j++){
					switch(currentTileLayer){
						case TileLayer.FloorMap:
							tileGrid[currentMapRow][j].floor=Floor.GetTile(tiles[j]);
							break;
						case TileLayer.CeilingMap:
							tileGrid[currentMapRow][j].ceiling=Ceiling.GetTile(tiles[j]);
							break;
						case TileLayer.PropMap:
							tileGrid[currentMapRow][j].prop=Prop.GetTile(tiles[j]);
							break;
							
					}
					
				}
				currentMapRow++;
			}
			
		}
	}

	public static Direction GetDirection(int angle){
		switch(angle){
			case 0:return Direction.North;
			case 90:return Direction.East;
			case 180:return Direction.South;
			case 270:return Direction.West;
			default:
				Debug.Log("Invalid angle for direction "+angle);
				return Direction.North;
		}
	}
}

public enum Direction{
	North,
	East,
	South,
	West
}