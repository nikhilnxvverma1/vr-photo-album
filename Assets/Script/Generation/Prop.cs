using UnityEngine;

public class Prop : MonoBehaviour {

	public PropType type;
	public Direction direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Prop GetTile(string tileValue){
		if(tileValue==null || tileValue.Trim().Length==0){
			return null;
		}
		Prop prop=new Prop();
		string[] typeAndAngle=tileValue.Split('-');
		string typeString=typeAndAngle[0];		
		switch(typeString){
			case "Piano":
				prop.type=PropType.Piano;
				break;
			case "Table":
				prop.type=PropType.Table;
				break;
			case "Chair":
				prop.type=PropType.Chair;
				break;
			case "FlowerPot":
				prop.type=PropType.FlowerPot;
				break;
			case "Stool":
				prop.type=PropType.Stool;
				break;				
			default:
				Debug.Log("Invalid Prop type : "+typeString);				
				break;
		}

		//convert the angle into direction
		int angle=int.Parse(typeAndAngle[1]);
		prop.direction=Tilemap.GetDirection(angle);
		return prop;
	}
}

public enum PropType{
	Piano,
	Table,
	Chair,
	FlowerPot,
	Stool
	//etc, more as they are thought of
}