using UnityEngine;
using System.Collections.Generic;

public class CreatePlanes : MonoBehaviour {

	// Use this for initialization
	public float numPlanes, initialHeight, separation, planescale;
	public bool hasMovement;
	private List<GameObject> planes;
	void Start () {
		planes = new List<GameObject>();
		for (int i = 0; i < numPlanes; i++)
		{
			GameObject plane = new GameObject();
			plane.transform.parent = this.transform;
			plane.transform.localPosition = new Vector3(0, initialHeight + i * separation, 0);
			var curPlane = plane.AddComponent<CreatePhotoPlane>();
			curPlane.planescale = planescale;
			planes.Add(plane);
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject p in planes)
		{
			if (hasMovement)
			{
				if (p.transform.position.y - 1 <= initialHeight)
				{
					p.transform.Translate(0, numPlanes * separation - 1, 0);
				}
				else
				{
					p.transform.Translate(0, -1 * Time.deltaTime / 2, 0);
				}
			}
		}
	}
}
