using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Select : MonoBehaviour {

	public GameObject hoeveredObeject;
	public GameObject door;
	public int timeRemaining = 2;
	// Use this for initialization
	void Start () {
		door = GameObject.FindWithTag("exitdoor");
	}

	// Update is called once per frame
	void Update () {

		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
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




	void countDown(){
		timeRemaining--;
		if (timeRemaining <= 0) {
			ChangeScene ("Lobby");
			CancelInvoke ("countDown");
			timeRemaining = 2;
			print ("Reset time");
		}
	}
	public void ChangeScene(string sceneName){
		Debug.Log ("change scene");
        SceneManager.LoadScene(sceneName);
	}

	public void Mouseover(){
		InvokeRepeating ("countDown", 1, 1);
	}

	public void MouseOut(){
		CancelInvoke ("countDown");
		timeRemaining = 2;
	}
}
