using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnterArtGallery : MonoBehaviour {

	private GameObject[] doors;
	// Use this for initialization
	void Start()
	{
//		doors = GameObject.FindGameObjectsWithTag("Door");
	}

	// Update is called once per frame
	void Update()
	{
//		foreach (GameObject d in doors)
//		{
//			var sprite = d.transform.FindChild("ExitIcon").gameObject;
//			sprite.SetActive(false);
//		}
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.root.gameObject;
//			Debug.Log (hitObject.tag);
//			if (hitObject.tag == "doors")
//			{
//				var sprite = hitObject.transform.FindChild("ExitIcon").gameObject;
//				sprite.SetActive(true);
//			}
			if (hitObject.tag == "doors"){
				Debug.Log ("Looking at door press Enter to enter");
				if (Input.GetKeyDown (KeyCode.Return)) {
					Debug.Log ("Doing Door actions");
					var doorInfo = hitObject.GetComponent<DoorInfo> ();
					DataScan.currentAlbum = DataScan.rootModel.albumList [doorInfo.albumIndex];
					Debug.Log ("Selected Album now:" + DataScan.currentAlbum.name);
					SceneManager.LoadScene ("ArtGallery");
				}
			}




		}


	}
}
