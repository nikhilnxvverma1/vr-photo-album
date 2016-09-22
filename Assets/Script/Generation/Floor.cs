using UnityEngine;

public class Floor : MonoBehaviour {

	public FloorType type;
	public Direction direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
	
public enum FloorType{
	Blank,
	Wall,
	Door,
	Window,
	Corner,
	//etc, more as they are thought of
	
}