using UnityEngine;
using System.Collections.Generic;

public class CreatePhotoPlane : MonoBehaviour {

	// Use this for initialization
	public float planex, planey, planez, planescale;
	void Start () {
		byte[] image = System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + @"\Assets\Starsinthesky.jpg");
		Texture2D t2 = new Texture2D(3877, 2842);
		t2.LoadImage(image);
		planex = 0; planey = 0; planez = 0; planescale = 5;
		List<GameObject> photos = new List<GameObject>();
		System.Random r = new System.Random();
		for (int i = 0; i < 10; i++)
		{
			GameObject photo = GameObject.CreatePrimitive(PrimitiveType.Quad);
			float px = (float)r.NextDouble() * planescale*planescale - planescale*planescale/2;
			float pz = (float)r.NextDouble() * planescale*planescale - planescale*planescale/2;
			photo.transform.position = new Vector3(px, planey, pz);
			photo.transform.eulerAngles = new Vector3(-90, 0, 0);

			Material mat = new Material(Shader.Find("Unlit/Texture"));
			mat.SetTexture("_MainTex", t2);
			Renderer rend = photo.GetComponent<Renderer>();
			rend.material = mat;
			photos.Add(photo);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
