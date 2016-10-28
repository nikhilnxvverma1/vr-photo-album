using UnityEngine;
using System.Collections;

public class RayCastRedDot : MonoBehaviour {

	public GameObject hoeveredObeject;
	public GameObject door;
	public Camera camera;
	public GameObject prefabRed;
	private bool lookingAtPicture = false;
	private GameObject picture;
	// Use this for initialization
	void Start () {
		door = GameObject.FindWithTag("doors");
//		Debug.Log(DataScan.currentAlbum.photoList[0].description);
	}

	// Update is called once per frame
	void Update () {

		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var ray = new Ray(camera.transform.position, camera.transform.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo)) {
			GameObject hitObject = hitInfo.transform.root.gameObject;
			if (hitObject.tag == "Picture" && hitInfo.distance<10) {
				Debug.Log ("Hovered  - " + hitObject.tag + ", " + hitInfo.distance);
				//GetComponent<Renderer>().material.color = Color.cyan;
				var r = hitObject.GetComponent<Renderer> ();
				Debug.Log ("Center "+r.bounds.center);
				Debug.Log ("Min "+ r.bounds.min);
				Debug.Log ("Extends "+ r.bounds.extents);
				Material mat = new Material (Shader.Find ("Unlit/UnlitAlphaWithFade"));
				mat.SetTexture ("_MainTex", DataScan.currentAlbum.photoList [0].texture);
				r.material = mat;
				if (!lookingAtPicture) {
					r.transform.localScale = r.transform.localScale * 1.3f;
					lookingAtPicture = true;
					picture = hitObject;
					var obj = picture.GetComponentInChildren<TextMesh> ();
					var picInfo = hitObject.GetComponent<PictureInfo> ();
					wrapme(obj,DataScan.currentAlbum.photoList[picInfo.pictureIndex].description);
				}
			} else {
				if (lookingAtPicture) {
					var r = picture.GetComponent<Renderer> ();
					r.transform.localScale = r.transform.localScale / 1.3f;
					var obj = picture.GetComponentInChildren<TextMesh> ();
					var picInfo = hitObject.GetComponent<PictureInfo> ();
					obj.text = "";
					lookingAtPicture = false;
				}
			}
			prefabRed.transform.position = hitInfo.point;
			float val = hitInfo.distance/500;
			prefabRed.transform.localScale = new Vector3 (val, val, val);
		}

	}

	public void wrapme(TextMesh t, string text){

		string builder = "";
		t.text = "";
		float rowLimit = 0.8f; //find the sweet spot 
		var sp = picture.GetComponent<SpriteRenderer>();
		rowLimit = sp.sprite.bounds.extents.x*1.0f;
		//		if(sp.bounds.center > sp.bounds.extents)
		t.transform.position = new Vector3(sp.bounds.min.x-0.3f, sp.bounds.min.y, sp.bounds.min.z);
		//		string text = "This is some text we'll use to demonstrate word wrapping. It would be too easy if a proper wrapping was already implemented in Unity :)";
		string[] parts = text.Split(' ');
		float x = t.characterSize;
		float offset = 0.0f;
		for (int i = 0; i < parts.Length; i++)
		{
			//			Debug.Log(parts[i]);
			offset += x;
			t.text += parts[i] + " ";
			if (offset > rowLimit)
			{
				t.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
				offset = 0.0f;
			}
			builder = t.text;
		}
	}


}