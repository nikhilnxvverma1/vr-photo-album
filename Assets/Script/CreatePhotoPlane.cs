using UnityEngine;
using System.Collections.Generic;

public class CreatePhotoPlane : MonoBehaviour
{
	public class Point
	{
		public float x;
		public float z;
		public Point(float x, float z)
		{
			this.x = x;
			this.z = z;
		}
	}
	// Use this for initialization
	public float planescale;
	public System.Random rng; 
	private List<GameObject> _photos;
	public int curPlane;
	public Photo[] photoList;
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
		
		_photos = new List<GameObject>();
		var positions = new List<Point>();
		var count = 0;
		for (int i = curPlane; i < curPlane + 6; i++)
		{
			var cur = i;
			if (cur >= photoList.Length)
			{
				cur = 0 + count;
				count++;
			}
			var p = photoList[cur];
			GameObject photo = GameObject.CreatePrimitive(PrimitiveType.Quad);
			photo.transform.parent = this.transform;
			float px;
			float pz;
			int kill = 0;
			do {
				
				px = (float)rng.NextDouble() * planescale * planescale - planescale * planescale / 2;
				pz = (float)rng.NextDouble() * planescale * planescale - planescale * planescale / 2;
				kill++;
				if (kill > 2000)
				{
					break;
				}
			} while (IsOverlapping(px, pz, positions));
			positions.Add(new Point(px, pz));
			photo.transform.localPosition = new Vector3(px, 0, pz);
			photo.transform.eulerAngles = new Vector3(-90, 0, 0);
			photo.transform.localScale = new Vector3(2.5f, 2.5f, 1);
			Material mat = new Material(Shader.Find("Unlit/UnlitAlphaWithFade"));
			mat.SetTexture("_MainTex", p.texture);
			Renderer rend = photo.GetComponent<Renderer>();
			rend.material = mat;
			_photos.Add(photo);
		}

	}

	

	// Update is called once per frame
	void Update()
	{
		
	}

	public bool IsOverlapping(float px, float pz, List<Point> positions)
	{
		foreach (Point p in positions)
		{
			if(px >= p.x - 3 && px <= p.x + 3 || pz >= p.z - 3 && pz <= p.z + 3)
			{
				return true;
			}
		}
		return false;
	}
}
