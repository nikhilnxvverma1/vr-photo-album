using UnityEngine;

public class Floor {

	public FloorType type;
	public Direction direction;

	public Floor(){
	}

	public Floor(FloorType type){
		this.type=type;
	}

	public Floor(FloorType type,Direction direction){
		this.type=type;
		this.direction=direction;
	}

	public static Floor GetTile(string tileValue){
		if(tileValue==null || tileValue.Trim().Length==0){
			return null;
		}
		Floor floor=new Floor();
		string[] typeAndAngle=tileValue.Split('-');
		string typeString=typeAndAngle[0];		
		switch(typeString){
			case "Blank":
				floor.type=FloorType.Blank;
				break;
			case "Wall":
				floor.type=FloorType.Wall;
				break;
			case "Corner":
				floor.type=FloorType.Corner;
				break;
			case "Door":
				floor.type=FloorType.Door;
				break;
			case "Window":
				floor.type=FloorType.Window;
				break;				
			default:
				Debug.Log("Invalid Floor type,setting blank : "+typeString);
				floor.type=FloorType.Blank;
				break;
		}

		//convert the angle into direction
		int angle=int.Parse(typeAndAngle[1]);
		floor.direction=Tilemap.GetDirection(angle);
		return floor;
	}

	override public string ToString(){
		return Floor.StringFor(type)+"-"+Tilemap.AngleFor(direction);
	}

	public static string StringFor(FloorType type){
		switch(type){
			case FloorType.Blank:
				return "Blank";				
			case FloorType.Wall:
				return "Wall";
			case FloorType.Corner:
				return "Corner";
			case FloorType.Door:
				return "Door";
			case FloorType.Window:
				return "Window";		
			default:
				return "Unknown";
		}
	}
}
	
public enum FloorType{
	Blank,
	Wall,
	Door,
	Window,
	Corner,
	//etc, more as they are thought of
	
}