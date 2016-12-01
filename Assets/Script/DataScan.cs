using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataScan : MonoBehaviour {

	private string rootFolder = "Assets/Images/Data Images";
	private string musicFolder = "Assets/Resources";
	public static RootModel rootModel;
	public static Album currentAlbum;
	public static OS_TYPE OS;
	public enum OS_TYPE{MAC,WINDOWS};
	private int width = 320;				// width and height for slideshow sprites
	private int height = 240;
	public int index;
	public static List<string> musicList = new List<string> ();

	private void LoadSprites(Album album){
		var slides = new Sprite[album.photoList.Length];
		for (int i = 0; i < album.photoList.Length; i++) {
			var tex = ScaleTexture (album.photoList [i].texture, width, height);
			slides [i] = Sprite.Create (tex, new Rect (0, 0, width, height), 
				new Vector2 (0.5f, 0.0f), 1.0f);
		}
		album.slides = slides;
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


	// Use this for initialization
	void Start () {
		if (rootModel == null) {
			rootModel = new RootModel ();
			buildRootModel ();
			currentAlbum = rootModel.albumList [0];
		}
		if (SystemInfo.operatingSystem.Contains ("Windows"))
			OS = OS_TYPE.WINDOWS;
		else
			OS = OS_TYPE.MAC;
		loadMusic ();
        DontDestroyOnLoad(gameObject);
//		printRootModel ();
	}

	private void loadMusic(){
		musicList = new List<string> ();
		DirectoryInfo dir = new DirectoryInfo(musicFolder);
		Debug.Log (dir);
		var info = dir.GetFiles("player_*.mp3");
		Debug.Log (info.Length);
		for (int i = 0; i < info.Length; i++) {
			
			musicList.Add(Path.GetFileNameWithoutExtension(info [i].Name));
		}
	}
	public static void printRootModel(){
		for (int i = 0; i < rootModel.albumList.Length; i++) {
			Debug.Log ("Folder "+rootModel.albumList[i].name+" has:"	);
			for (int j = 0; j < rootModel.albumList[i].photoList.Length; j++) {
				Debug.Log (rootModel.albumList [i].photoList [j].name);
			}
		}
	}
	private void buildRootModel (){
		DirectoryInfo dir = new DirectoryInfo(rootFolder);
		DirectoryInfo[] dirs =  dir.GetDirectories ();
		rootModel.albumList = new Album[dirs.Length+1];
		rootModel.albumList[0] = new Album ();
		rootModel.albumList[0].path = rootFolder;
		rootModel.albumList[0].name = "Root";
		loadtoAlbum (rootModel.albumList[0], dir);

		for (int i = 0; i < dirs.Length; i++) {
			rootModel.albumList[i+1] = new Album ();
			rootModel.albumList[i+1].path = dirs[i].FullName;
			rootModel.albumList [i + 1].name = dirs [i].Name;
			loadtoAlbum (rootModel.albumList[i+1], dirs[i]);
		}
	}

	private static Texture2D LoadPNG(string filePath, Photo p) {

		Texture2D tex = new Texture2D(2,2);
		byte[] fileData;
		if (File.Exists (filePath)) {
			fileData = File.ReadAllBytes (filePath);
			p.byteArray = fileData;
			tex.LoadImage (fileData);
			//			Debug.Log ("loaded files");
		} else {
			//			Debug.Log ("Did not load files: "+filePath);
		}
		return tex;
	}
	private void buildDictionary(string filepath, Dictionary<string, string> dict){
		string line;
		// Read the file and display it line by line.
		System.IO.StreamReader file = 
			new System.IO.StreamReader(filepath);
		while((line = file.ReadLine()) != null)
		{
			string[] parts = line.Split (new char[]{ ',' }, 2);
			//Debug.Log (parts.Length+" *"+parts [0]+"->"+parts [1]);
			dict.Add (parts [0], parts [1]);
		}

		file.Close();
	}

	private void loadtoAlbum(Album album, DirectoryInfo dir){
		FileInfo[] info = dir.GetFiles("*.txt");
		bool hasDesc = info != null && info.Length > 0;
		Dictionary<string, string> dictionary = new Dictionary<string, string> ();
		if (hasDesc) {
			buildDictionary (info [0].FullName, dictionary);
		}
		info = dir.GetFiles("*.mp3");
		bool hasAudio = info != null && info.Length > 0;
		if (hasAudio) {
//			album.audio = info [0].Name.Split(new char[]{'.'},2)[0];
			if (album.name != "Root")
				album.audio = rootFolder + "/" + album.name + "/" + info [0].Name;
			else
				album.audio = rootFolder + "/" + info [0].Name;
		}
		info = dir.GetFiles("*.png");
		album.photoList = new Photo[info.Length];
		for (int i = 0; i < info.Length; i++) {
			album.photoList [i] = new Photo ();
			album.photoList [i].name = info [i].Name;
			Texture2D t = LoadPNG(info[i].FullName, album.photoList[i]);
			album.photoList [i].width = t.width;
			album.photoList [i].height = t.height;
			album.photoList [i].texture = t;
			if (dictionary.ContainsKey (info [i].Name)) {
				album.photoList [i].description = dictionary [info [i].Name];
			} else {
				album.photoList [i].description = "";
			}
		}
		LoadSprites (album);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
