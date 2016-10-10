using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SlideShow : MonoBehaviour {

	public Album album;
	private Texture2D[] slides = new Texture2D[10];
	public Image imageObj;
	private float changeTime = 3.0f;
	private int currentSlide = 0;
	private float timeSinceLastUpdate;
	private int width;
	private int height;

	void Start()
	{
//		imageObj = GetComponent<Image>();
		DataScan ds = GetComponent<DataScan>();
		//var ds = this.GetComponentInParent<DataScan>();
//		if (ds != null) {
			var albums = ds.rModel.albumList;
			album = albums [1];
//			ds.printrModel ();
			currentSlide++;
			width = (int)imageObj.rectTransform.rect.width;
			height = (int)imageObj.rectTransform.rect.height;
			LoadTextures ();
			Sprite sprite = Sprite.Create (slides [currentSlide], new Rect (0, 0, width, height), new Vector2 (0.5f, 0.0f), 1.0f);
			imageObj.sprite = sprite;
			currentSlide = currentSlide + 1 % slides.Length;
			timeSinceLastUpdate = 0.0f;
//		}
	}

	private void LoadTextures(){
		for (int i = 0; i < 10 && i < album.photoList.Length; i++) {
			slides [i] = LoadPNG (album.path + "/"+album.photoList[i].name, width, height);
//			slides[i] = album.photoList[i].texture;
		}
	}

	public static Texture2D LoadPNG(string filePath, int width, int height) {

		Texture2D tex = new Texture2D(2,2);
		byte[] fileData;
		if (File.Exists (filePath)) {
			fileData = File.ReadAllBytes (filePath);
			tex.LoadImage (fileData);
			tex = ScaleTexture(tex,width,height);
			Debug.Log ("loaded files");
		} else {
			Debug.Log ("Did not load files: "+filePath);
		}
		return tex;
	}

	private static Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
		Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
		Color[] rpixels=result.GetPixels(0);
		float incX=((float)1/source.width)*((float)source.width/targetWidth);
		float incY=((float)1/source.height)*((float)source.height/targetHeight);
		for(int px=0; px<rpixels.Length; px++) {
			rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth),
				incY*((float)Mathf.Floor(px/targetWidth)));
		}
		result.SetPixels(rpixels,0);
		result.Apply();
		return result;
	}

	void Update()
	{
		if(timeSinceLastUpdate > changeTime &&  currentSlide < slides.Length)
		{
			// below 6 lines can be used to test the code on dummy data, else comment our
//			if(currentSlide%2==0)
//				slides[currentSlide] = LoadPNG("/Users/arjundhuliya/Documents/vr-photo-album/Assets/Images/GreatBall.png",
//					width,height);
//			else
//				slides[currentSlide] = LoadPNG("/Users/arjundhuliya/Documents/vr-photo-album/Assets/Images/yoshi.png",
//					width,height);	


			Sprite sprite = Sprite.Create(slides[currentSlide], new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
			imageObj.sprite = sprite;
			currentSlide = currentSlide+1 % slides.Length;
			timeSinceLastUpdate = 0.0f;
		}
		timeSinceLastUpdate += Time.deltaTime;
	}
}