using UnityEngine;
using System.Collections;

public class RayCastRedDot : MonoBehaviour {

	public GameObject hoeveredObeject;
	public GameObject door;
	public Camera camera;
	public GameObject prefabRed;
	private bool lookingAtPicture = false;
	private Renderer picture;
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
			if (hitObject.tag == "Picture") {
				Debug.Log ("Hovered  - " + hitObject.tag + ", " + hitInfo.distance);
				//GetComponent<Renderer>().material.color = Color.cyan;
				var r = hitObject.GetComponent<Renderer> ();
				Material mat = new Material (Shader.Find ("Unlit/UnlitAlphaWithFade"));
				mat.SetTexture ("_MainTex", DataScan.currentAlbum.photoList [0].texture);
				r.material = mat;
				if (!lookingAtPicture) {
					r.transform.localScale = r.transform.localScale * 1.5f;
					lookingAtPicture = true;
					picture = r;
					var obj = hitObject.GetComponentInChildren<TextMesh> ();
//					obj.text = DataScan.currentAlbum.photoList[0].description;
					wrapme(obj,DataScan.currentAlbum.photoList[0].description); 
				
				}
			} else {
				if (lookingAtPicture) {
//					var r = hitObject.GetComponent<Renderer> ();
					picture.transform.localScale = picture.transform.localScale / 1.5f;
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
		float rowLimit = 1.2f; //find the sweet spot    
//		string text = "This is some text we'll use to demonstrate word wrapping. It would be too easy if a proper wrapping was already implemented in Unity :)";
		string[] parts = text.Split(' ');
		float x = t.characterSize;
		float offset = 0.0f;
		for (int i = 0; i < parts.Length; i++)
		{
			Debug.Log(parts[i]);
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

	void SelectObject(GameObject obj)
	{
		if (hoeveredObeject != null)
		{
			if(obj == hoeveredObeject)
				return;
			ClearSelection();
		}
		hoeveredObeject = obj;
		Renderer[] rs = hoeveredObeject.GetComponentsInChildren<Renderer>();
		//if (hoeveredObeject == door)
		//{
		foreach (Renderer r in rs)
		{
			Material m = r.material;
			m.color = Color.green;
			r.material = m;
		}
		//}
	}

	void ClearSelection()
	{
		if (hoeveredObeject == null)
			return;

		Renderer[] rs = hoeveredObeject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in rs)
		{
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}
		hoeveredObeject = null;
	}
}