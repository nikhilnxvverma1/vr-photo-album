using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SlideShow : MonoBehaviour {

	public Album album;
	private Sprite[] slides;
	private float changeTime = 3.0f;
	private int currentSlide = 0;
	private float timeSinceLastUpdate;
//	private int width = 320;
//	private int height = 240;
	public int index;
	private float deltaTime;

	void Start()
	{
		album = DataScan.rootModel.albumList[index];
//		LoadSprites ();
		slides = album.slides;
		if (slides.Length > 0) {
			Sprite sprite = slides [currentSlide];
			GetComponent<SpriteRenderer> ().sprite = sprite;
			currentSlide = currentSlide + 1 % slides.Length;
		}
		var rd = Random.Range (0.0f, 0.00f);
		deltaTime = Time.deltaTime + rd;
		changeTime += Random.Range (-0.2f, 1.2f);
		timeSinceLastUpdate = 0.0f;
	}

	void Update()
	{
		if (slides.Length > 0) {
			if (timeSinceLastUpdate > changeTime && currentSlide < slides.Length) {	
				//			Sprite sprite = Sprite.Create(slides[currentSlide], new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
				GetComponent<SpriteRenderer> ().sprite = slides [currentSlide];
				currentSlide = (currentSlide + 1) % slides.Length;
				timeSinceLastUpdate = 0.0f;
			}
			timeSinceLastUpdate += deltaTime;
		}
	}

//	private void LoadSprites(){
//		slides = new Sprite[album.photoList.Length];
//		for (int i = 0; i < album.photoList.Length; i++) {
//			var tex = ScaleTexture (album.photoList [i].texture, width, height);
//			slides [i] = Sprite.Create (tex, new Rect (0, 0, width, height), 
//															new Vector2 (0.5f, 0.0f), 1.0f);
//		}
//	}


//	private void LoadTextures(){
//		slides = new Texture2D[album.photoList.Length];
//		for (int i = 0; i < album.photoList.Length; i++) {
//			var tex = LoadPNG (album.path + "/"+album.photoList[i].name, width, height);
//		}
//	}

//	public static Texture2D LoadPNG(string filePath, int width, int height) {
//		Texture2D tex = new Texture2D(2,2);
//		byte[] fileData;
//		if (File.Exists (filePath)) {
//			fileData = File.ReadAllBytes (filePath);
//			tex.LoadImage (fileData);
//			tex = ScaleTexture(tex,width,height);
//			Debug.Log ("loaded files");
//		} else {
//			Debug.Log ("Did not load files: "+filePath);
//		}
//		return tex;
//	}

//	private static Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
//		Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
//		Color[] rpixels=result.GetPixels(0);
//		float incX=((float)1/source.width)*((float)source.width/targetWidth);
//		float incY=((float)1/source.height)*((float)source.height/targetHeight);
//		for(int px=0; px<rpixels.Length; px++) {
//			rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth),
//				incY*((float)Mathf.Floor(px/targetWidth)));
//		}
//		result.SetPixels(rpixels,0);
//		result.Apply();
//		return result;
//	}

}