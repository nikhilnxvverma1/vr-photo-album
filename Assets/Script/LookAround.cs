using UnityEngine;
using System.Collections;

public class LookAround : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float senstivity=5.0f;
	public float smoothing=2.0f;

	GameObject charecter;

	// Use this for initialization
	void Start () {
		charecter=this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction=new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"));
		direction=Vector2.Scale(direction,new Vector2(senstivity*smoothing,senstivity*smoothing));

		smoothV.x=Mathf.Lerp(smoothV.x,direction.x,1f/smoothing);
		smoothV.y=Mathf.Lerp(smoothV.y,direction.y,1f/smoothing);
		mouseLook+=smoothV;

		transform.localRotation=Quaternion.AngleAxis(-mouseLook.y,Vector3.right);
		charecter.transform.localRotation=Quaternion.AngleAxis(mouseLook.x,charecter.transform.up);
	}
}
