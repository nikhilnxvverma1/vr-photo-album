using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangeAlbumMenu : MonoBehaviour {

 	public GameObject plane;
	private List<Image> grids = new List<Image>();
	public Image image;
	private Vector2 separation = new Vector2 (1.5f, 1f);
	private int indexOfSelected = 0;
	int offset = 0;
	private bool changed = true;
	float imageWidth;
	float imageHeight;
	float lastX;
	float lastY;
	public GameObject canvas;


	// Use this for initialization
	void Start () {
		float lastX = Input.GetAxis("DPadX");
		float lastY = Input.GetAxis("DPadY");
		float panelWidth = GetComponentInParent<RectTransform> ().rect.width;
		float panelHeight = GetComponentInParent<RectTransform> ().rect.height;
		imageWidth = (panelWidth - (4*separation.x))/2;
		imageHeight = (panelHeight - (4 * separation.y)) / 2;
//		Debug.Log (panelWidth + "," + separation.x);
//		Debug.Log (imageWidth + "," + imageHeight);
		float x = - panelWidth / 4;
		float y = panelHeight / 4;
		float deltaX = panelWidth / 2;
		float deltaY = panelHeight / 2;
		foreach (Album a in DataScan.rootModel.albumList)
		{
			if (a.photoList.Length > 0) {
				Photo p = a.photoList [0];
				Image im = (Image)Instantiate (image);
				Debug.Log (x + "," + y);
				im.rectTransform.sizeDelta = new Vector2(imageWidth,imageHeight);
//				im.rectTransform.rect.height = imageHeight;
				im.transform.SetParent (plane.transform, false);
				im.rectTransform.localPosition = new Vector3(x, y,0);
				if (x + deltaX < panelWidth/2) {
					x += deltaX;
				}
				else{
					x = -panelWidth/4;
					y -= deltaY;
				}
				Material mat = new Material (Shader.Find ("Unlit/UnlitAlphaWithFade"));
				mat.SetTexture ("_MainTex", p.texture);
				im.material = mat;
				grids.Add (im);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		var previouslySelected = grids [indexOfSelected + offset];
		indexOfSelected = senseKeys ();
		if (changed) {
			changed = false;
			previouslySelected.rectTransform.sizeDelta = new Vector2 (imageWidth, imageHeight);
			var im = grids [indexOfSelected + offset];
			float panelWidth = GetComponentInParent<RectTransform> ().rect.width;
			float panelHeight = GetComponentInParent<RectTransform> ().rect.height;
			float w = (panelWidth - (separation.x/2))/2;
			float h = (panelHeight - (separation.y/2)) / 2;
			im.rectTransform.sizeDelta = new Vector2(w,h);
		}
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown ("joystick button 0") ||
			(DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown ("joystick button 16"))){
			Debug.Log ("Selected album" + (indexOfSelected + offset));
			DataScan.currentAlbum = DataScan.rootModel.albumList [indexOfSelected+offset];
			canvas.gameObject.SetActive(false);
		} 
	}
	private int senseKeys(){
		int change = 0;

		if(DataScan.OS == DataScan.OS_TYPE.MAC){
			if (Input.GetKeyDown ("joystick button 5")) { //up key
				change -= 2;
				Debug.Log ("UP");
			}
			if(Input.GetKeyDown ("joystick button 6")) //down key
				change +=2;
			if(Input.GetKeyDown ("joystick button 7")) //left key
				change -=1;
			if(Input.GetKeyDown ("joystick button 8")) //right key
				change +=1;
		}
		else{
			if (Input.GetAxis ("DPadX") == 1){
				change += 1;
				Debug.Log ("Right");
				}
			if(Input.GetAxis ("DPadX") == -1){
				change -= 1;
				Debug.Log ("Left");
			}
			if (Input.GetAxis ("DPadY") == 1){
				change -= 2;
				Debug.Log ("Up");
			}
			if(Input.GetAxis ("DPadY") == -1){
				change += 2;
				Debug.Log ("Down");
			}
		}
		if ((indexOfSelected + change >= 0) && ((indexOfSelected + change) < DataScan.rootModel.albumList.Length-1)) {
			if (change != 0) {
				changed = true;
//				Debug.Log (DataScan.rootModel.albumList.Length+",new Index" + (indexOfSelected + change));
			}
			return indexOfSelected + change;
		}
		else
			return indexOfSelected;
	}
}
