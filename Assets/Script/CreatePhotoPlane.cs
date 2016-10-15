using UnityEngine;
using System.Collections.Generic;

public class CreatePhotoPlane : MonoBehaviour
{

	// Use this for initialization
	public float planescale;
	private static System.Random rng = new System.Random();
	private List<GameObject> _photos;
	public List<GameObject> Photos
	{
		get { return _photos; }
		set { _photos = value; }
	}


	void Start()
	{
		//var slash = System.IO.Path.DirectorySeparatorChar;
		//byte[] image = System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + string.Format(@"{0}Assets{1}Starsinthesky.jpg",slash,slash));
		//Texture2D t2 = new Texture2D(3877, 2842);
		//t2.LoadImage(image);
		var albums = DataScan.rootModel.albumList;
		var curAlbum = DataScan.currentAlbum;
		var photoList = ShufflePhotos(curAlbum.photoList);
		_photos = new List<GameObject>();
		foreach (Photo p in photoList)
		{
			GameObject photo = GameObject.CreatePrimitive(PrimitiveType.Quad);
			photo.transform.parent = this.transform;
			float px = (float)rng.NextDouble() * planescale * planescale - planescale * planescale / 2;
			float pz = (float)rng.NextDouble() * planescale * planescale - planescale * planescale / 2;
			photo.transform.localPosition = new Vector3(px, 0, pz);
			photo.transform.eulerAngles = new Vector3(-90, 0, 0);

			Material mat = new Material(Shader.Find("Unlit/UnlitAlphaWithFade"));
			mat.SetTexture("_MainTex", p.texture);
			Renderer rend = photo.GetComponent<Renderer>();
			rend.material = mat;
			_photos.Add(photo);
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
		
	}

}
