using UnityEngine;
using System.Collections;
using System.IO;

public class DataScan : MonoBehaviour {

	private string rootFolder = "Assets/Images/Data Images";
	public static RootModel rootModel;
	public static Album currentAlbum;

	// Use this for initialization
	void Start () {
		rootModel = new RootModel ();
		buildRootModel ();
		currentAlbum = rootModel.albumList [1];
//		printRootModel ();
	}
	public void printRootModel(){
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
			rootModel.albumList[i+1].name = dirs[i].Name;
			loadtoAlbum (rootModel.albumList[i+1], dirs[i]);
//			Debug.Log (rootModel.albumList [i].name);
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

	private void loadtoAlbum(Album album, DirectoryInfo dir){
		FileInfo[] info = dir.GetFiles("*.png");
		album.photoList = new Photo[info.Length];
		for (int i = 0; i < info.Length; i++) {
			album.photoList [i] = new Photo ();
			album.photoList [i].name = info [i].Name;
//			Debug.Log ("In "+album.name+" Found :"+info [i].Name);
			Texture2D t = LoadPNG(info[i].FullName, album.photoList[i]);
			album.photoList [i].width = t.width;
			album.photoList [i].height = t.height;
			album.photoList [i].texture = t;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
