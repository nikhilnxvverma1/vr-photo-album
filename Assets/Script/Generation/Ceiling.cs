using UnityEngine;

public class Ceiling {

	public CeilingType type;
	public Direction direction;

	public static Ceiling GetTile(string tileValue){
		if(tileValue==null || tileValue.Trim().Length==0){
			return null;
		}
		Ceiling ceiling=new Ceiling();
		string[] typeAndAngle=tileValue.Split('-');
		string typeString=typeAndAngle[0];		
		switch(typeString){
			case "Blank":
				ceiling.type=CeilingType.Blank;
				break;
			case "Chandelier":
				ceiling.type=CeilingType.Chandelier;
				break;
			case "Vent":
				ceiling.type=CeilingType.Vent;
				break;
			case "Fan":
				ceiling.type=CeilingType.Fan;
				break;
			case "FireAlarm":
				ceiling.type=CeilingType.FireAlarm;
				break;		
			case "HangingBanner":
				ceiling.type=CeilingType.HangingBanner;
				break;		
						
			default:
				Debug.Log("Invalid Ceiling type,setting blank : "+typeString);
				ceiling.type=CeilingType.Blank;
				break;
		}

		//convert the angle into direction
		int angle=int.Parse(typeAndAngle[1]);
		ceiling.direction=Tilemap.GetDirection(angle);
		return ceiling;
	}

	public static string StringFor(CeilingType type){
		switch(type){
			case CeilingType.Blank:
				return "Blank";				
			case CeilingType.Chandelier:
				return "Chandelier";
			case CeilingType.Vent:
				return "Vent";
			case CeilingType.Fan:
				return "Fan";
			case CeilingType.FireAlarm:
				return "FireAlarm";		
			case CeilingType.HangingBanner:
				return "HangingBanner";	
			default:
				return "Unknown";
		}
	}

	override public string ToString(){
		return Ceiling.StringFor(type)+"-"+Tilemap.AngleFor(direction);
	}

}

public enum CeilingType{
	Blank, 
	Chandelier,
	Vent,
	Fan,
	FireAlarm,
	HangingBanner
	//etc, more as they are thought of
}