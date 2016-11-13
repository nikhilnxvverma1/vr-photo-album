using UnityEngine;
using System.Collections;

public class GenerateLobby : MonoBehaviour {

	// Use this for initialization
	public GameObject door, slideshow;
	public Sprite exitIcon;
	void Start () {
		Material walls = Resources.Load("Hardwood_Standard_Texture", typeof(Material)) as Material;
		Material floors = Resources.Load("Tile_Standard", typeof(Material)) as Material;
		int size = (int)System.Math.Ceiling( (DataScan.rootModel.albumList.Length-1) / 2.0);
		float scaleFactor = size*3f;
		float translate = -10 * scaleFactor / 2 + 21.7f;
		GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
		leftWall.GetComponent<Renderer>().material = walls;
		leftWall.name = "leftWall";
		leftWall.transform.localScale = new Vector3(scaleFactor, 1, 2.29f);
		leftWall.transform.localPosition = new Vector3(-26.6f, 11.5f, translate);
		leftWall.transform.eulerAngles = new Vector3(-90, 0, -90);
		GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
		rightWall.GetComponent<Renderer>().material = walls;
		rightWall.name = "rightWall";
		rightWall.transform.localScale = new Vector3(scaleFactor, 1, 2.29f);
		rightWall.transform.localPosition = new Vector3(26.9f, 11.5f, translate);
		rightWall.transform.eulerAngles = new Vector3(-90, 0, 90);
		GameObject backWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
		backWall.GetComponent<Renderer>().material = walls;
		backWall.name = "backWall";
		backWall.transform.localScale = new Vector3(5.45f, 1, 2.29f);
		backWall.transform.localPosition = new Vector3(.24f, 11.5f, -10*scaleFactor + 21.7f);
		backWall.transform.eulerAngles = new Vector3(90, 0,0);
		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
		floor.GetComponent<Renderer>().material = floors;
		floor.name = "floor";
		floor.transform.localScale = new Vector3(6, 1, scaleFactor);
		floor.transform.localPosition = new Vector3(-.653f, .11f, translate);
		GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		ceiling.GetComponent<Renderer>().material = walls;
		ceiling.name = "ceiling";
		ceiling.transform.localScale = new Vector3(6, 1, scaleFactor);
		ceiling.transform.localPosition = new Vector3(-.653f, 22.4f, translate);
		ceiling.transform.eulerAngles = new Vector3(-180, 0, 0);
		int i;
		for (i = 1; i <= size;i++)
		{
			float pos = -scaleFactor * 10 / size * i + 30f;
			GameObject curDoor = (GameObject)Instantiate(door, new Vector3(-25.9f, 0, pos),Quaternion.Euler(new Vector3(0,-90,0)));
			GameObject curIcon = new GameObject("ExitIcon");
			var sr = curIcon.AddComponent<SpriteRenderer>();
			sr.sprite = exitIcon;
			curIcon.transform.SetParent(curDoor.transform);
			curIcon.transform.localPosition = new Vector3(0, 30, -4);
			curIcon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
			curIcon.transform.localScale = new Vector3(1, 1, 1);
			var doorScript = curDoor.GetComponent<DoorInfo>();
			doorScript.albumIndex = i;
			GameObject curSlideshow = (GameObject)Instantiate(slideshow, new Vector3(-26.2f, 10.2f, pos), Quaternion.Euler(new Vector3(0, -90, 0)));
			var slideScript = curSlideshow.GetComponent<SlideShow>();
			slideScript.index = i;

		}
		int secHalf = (int)System.Math.Floor((DataScan.rootModel.albumList.Length-1) / 2.0);
		for (i=1;i<=secHalf; i++)
		{
			float pos = size > secHalf ? (-scaleFactor * 10 / size * i + 20f) : (-scaleFactor * 10 / size * i + 30f);
			GameObject curDoor = (GameObject)Instantiate(door, new Vector3(26.2f, 0, pos), Quaternion.Euler(new Vector3(0, 90, 0)));
			GameObject curIcon = new GameObject("ExitIcon");
			var sr = curIcon.AddComponent<SpriteRenderer>();
			sr.sprite = exitIcon;
			curIcon.transform.SetParent(curDoor.transform);
			curIcon.transform.localPosition = new Vector3(0, 30, -4);
			curIcon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
			curIcon.transform.localScale = new Vector3(1, 1, 1);
			var script = curDoor.GetComponent<DoorInfo>();
			script.albumIndex = i+size;
			GameObject curSlideshow = (GameObject)Instantiate(slideshow, new Vector3(26.2f, 10.2f, pos), Quaternion.Euler(new Vector3(0, 90, 0)));
			var slideScript = curSlideshow.GetComponent<SlideShow>();
			slideScript.index = i+size;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
