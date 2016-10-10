using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {

	public GameObject hoeveredObeject;
	public GameObject door;
	// Use this for initialization
	void Start () {
		door = GameObject.FindWithTag("doors");
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.root.gameObject;

			Debug.Log("Mouse is over" + hitInfo.collider.name);

			SelectObject(hitObject);

		}
		else {
			ClearSelection();
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
		if (hoeveredObeject == door)
		{
			foreach (Renderer r in rs)
			{
				Material m = r.material;
				m.color = Color.green;
				r.material = m;
			}
		}




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

