using UnityEngine;
using System.Collections;

public class PictureInfo : MonoBehaviour {

	public int pictureIndex;
	public int width;
	public int height;
	public SpriteRenderer sp;
	// Use this for initialization
	void Start () {
		var photo = DataScan.rootModel.albumList[1].photoList [pictureIndex];
		sp = GetComponent<SpriteRenderer> ();
//		var tex = ScaleTexture (photo.texture, width, height);	
		var tex = photo.texture;
		sp.sprite = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), 
			new Vector2 (0.50f, 0.50f), tex.height);
		var text = GetComponentInChildren<TextMesh> ();
		text.text = "";

//		if(photo.description.Length>0){ 
//			var text = GetComponentInChildren<TextMesh> ();
////			wrapme(text, photo.description);
//		}

	}

	public void wrapme(TextMesh t, string text){

		string builder = "";
		t.text = "";
		float rowLimit = 0.8f; //find the sweet spot 
		rowLimit = sp.sprite.bounds.extents.x*1.0f;
//		if(sp.bounds.center > sp.bounds.extents)
		t.transform.position = sp.bounds.min;
		//		string text = "This is some text we'll use to demonstrate word wrapping. It would be too easy if a proper wrapping was already implemented in Unity :)";
		string[] parts = text.Split(' ');
		float x = t.characterSize;
		float offset = 0.0f;
		for (int i = 0; i < parts.Length; i++)
		{
//			Debug.Log(parts[i]);
			offset += x;
			t.text += parts[i] + " ";
			if (offset > rowLimit)
			{
				t.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
				offset = 0.0f;
			}
			builder = t.text;
		}
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
