using UnityEngine;
using System.Collections;

public class RayCastRedDot : MonoBehaviour {

	public GameObject hoeveredObeject;
	public GameObject door;
	public Camera camera;
	public GameObject prefabRed;
	// Use this for initialization
	void Start () {
		door = GameObject.FindWithTag("doors");
	}

	// Update is called once per frame
	void Update () {

		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var ray = new Ray(camera.transform.position, camera.transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.root.gameObject;
			if (hitObject.tag == "Picture") {
				Debug.Log ("Hovered  - " + hitObject.tag + ", " + hitInfo.distance);
				//GetComponent<Renderer>().material.color = Color.cyan;
				var r = hitObject.GetComponent<Renderer>();
				Material mat = new Material(Shader.Find("Unlit/UnlitAlphaWithFade"));
				mat.SetTexture("_MainTex", DataScan.rootModel.albumList[2].photoList[1].texture);
				r.material = mat;
			}
			prefabRed.transform.position = hitInfo.point;

			//			Debug.Log ("Creating Red point "+hitInfo.point);
			//			Debug.Log ("Creating Red normal "+hitInfo.normal);
			//			Instantiate( prefabRed, hitInfo.point, Quaternion.identity );

			//			else
			//				Debug.Log ("oops no Hovered");


			//			Debug.Log("Mouse is over" + hitInfo.collider.name);

			//			SelectObject(hitObject);
			//
		}
		//		else {
		//			ClearSelection();
		//		}


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
