using UnityEngine;
using System.Collections.Generic;

public class CreatePlanes : MonoBehaviour
{

	// Use this for initialization
	public float numPlanes, initialHeight, separation, planescale, imageSize;
	public bool hasMovement;
	private static System.Random rng = new System.Random();
	private List<GameObject> planes;
	public int numPhotos;
	void Start()
	{
		planes = new List<GameObject>();
		var curAlbum = DataScan.currentAlbum;
		var photoList = ShufflePhotos(curAlbum.photoList);
		for (int i = 0; i < numPlanes; i++)
		{
			GameObject plane = new GameObject();
			plane.transform.parent = this.transform;
			plane.transform.localPosition = new Vector3(0, initialHeight + i * separation, 0);
			var curPlane = plane.AddComponent<CreatePhotoPlane>();
			curPlane.planescale = planescale;
			curPlane.curPlane = i;
			curPlane.rng = rng;
			curPlane.numPhotos = numPhotos;
			curPlane.photoList = photoList;
			curPlane.size = imageSize;
			planes.Add(plane);
		}
	}

	public Photo[] ShufflePhotos(Photo[] list)
	{
		int n = list.Length;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Photo t = list[k];
			list[k] = list[n];
			list[n] = t;
		}
		return list;
	}

	// Update is called once per frame
	void Update()
	{
		foreach (GameObject p in planes)
		{
			if (hasMovement)
			{
				if (p.transform.position.y - 1 <= initialHeight)
				{
					var cpp = p.GetComponent<CreatePhotoPlane>();
					bool done = false;
					if (cpp != null && cpp.Photos != null) {
						foreach (GameObject photo in cpp.Photos) {
							var rend = photo.GetComponent<Renderer> ();
							var color = rend.material.color;
							if (color.a <= 0) {
								done = true;
							}
							color.a -= 1 / (separation / Time.deltaTime / 2);
							rend.material.color = color;
						}
						if (done) {
							foreach (GameObject photo in cpp.Photos) {
								var rend = photo.GetComponent<Renderer> ();
								var color = rend.material.color;
								color.a = 1;
								rend.material.color = color;
							}
							p.transform.Translate (0, numPlanes * separation - 1, 0);
							cpp.ChangePhotos ();
						}
					}


				}
				else
				{
					p.transform.Translate(0, -1 * Time.deltaTime / 2, 0);
				}
			}

		}
	}


}
