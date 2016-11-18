using UnityEngine;
using System.Collections;

public class ScalePicturesOnFocus : MonoBehaviour {

	private GameObject[] pictures;
	public float focusedScale=1.8f;
	public float defaultScale=1.5f;
	public float distanceForFocus;


	// Use this for initialization
	void Start () {
		pictures = GameObject.FindGameObjectsWithTag("Picture");
	}
	
	// Update is called once per frame
	void Update () {
		if(pictures.Length==0){
			pictures = GameObject.FindGameObjectsWithTag("Picture");
		}

		foreach (GameObject d in pictures)
		{
			d.transform.localScale=new Vector3(defaultScale,defaultScale,1);
		}

		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo))
		{
			GameObject hitObject = hitInfo.transform.root.gameObject;
			if ((hitObject.tag == "Picture")&&(hitInfo.distance < distanceForFocus))
			{
				hitObject.transform.localScale=new Vector3(focusedScale,focusedScale,1);
			}
		}
	}
}
