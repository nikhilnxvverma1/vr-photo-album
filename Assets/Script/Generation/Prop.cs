using UnityEngine;

public class Prop {

	public PropType type;
	public Direction direction;

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

	public static string StringFor(PropType type){
		switch(type){
			case PropType.Piano:
				return "Piano";				
			case PropType.Table:
				return "Table";
			case PropType.Chair:
				return "Chair";
			case PropType.FlowerPot:
				return "FlowerPot";
			case PropType.Stool:
				return "Stool";		
			default:
				return "Unknown";
		}
	}

	override public string ToString(){
		return Prop.StringFor(type)+"-"+Tilemap.AngleFor(direction);
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