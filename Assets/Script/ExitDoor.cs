using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour {
	public  GameObject selectedObject;
	public GameObject door;
	public int timeRemaining = 2;

	void Start(){		
		door = GameObject.FindWithTag ("exitdoor");	
	}
	void update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.Log ("checking for object collision");
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo)) {

			GameObject hitObject = hitInfo.transform.root.gameObject;
			Debug.Log ("Hitting a game object " + hitObject.name);

			SelectObject (hitObject);
		}
		else{
			ClearSelection ();
		}
	}

	void SelectObject(GameObject obj){
		if (selectedObject != null) {
			if (obj == selectedObject)
				return;
			ClearSelection ();
		}
		selectedObject = obj;
		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = Color.green;
			r.material = m;
		}
		if (selectedObject == door) {
			countDown ();
		}
	}
	void ClearSelection(){
		selectedObject = null;
		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}
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
		//		Application.LoadLevel (sceneName);
	}

	public void Mouseover(){
		InvokeRepeating ("countDown", 1, 1);
	}

	public void MouseOut(){
		CancelInvoke ("countDown");
		timeRemaining = 2;
	}
}
