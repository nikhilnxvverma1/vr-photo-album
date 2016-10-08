using UnityEngine;
using System.Collections;
using System.IO;

public class DataScan : MonoBehaviour {

	private string rootFolder = "Assets/Images/Data Images";
	public RootModel rModel;

	// Use this for initialization
	void Start () {
		rModel = new RootModel ();
		buildRootModel ();
		printrModel ();
	}
	private void printrModel(){
		for (int i = 0; i < rModel.albumList.Length; i++) {
			Debug.Log ("Folder "+rModel.albumList[i].name+" has:"	);
			for (int j = 0; j < rModel.albumList[i].photoList.Length; j++) {
				Debug.Log (rModel.albumList [i].photoList [j].name);
//				Debug.Log (	rModel.albumList[i].photoList[j].texture.ToString());
			}
		}
	}
	private void buildRootModel (){
		DirectoryInfo dir = new DirectoryInfo(rootFolder);
		DirectoryInfo[] dirs =  dir.GetDirectories ();
		rModel.albumList = new Album[dirs.Length+1];
		rModel.albumList[0] = new Album ();
		rModel.albumList[0].path = rootFolder;
		rModel.albumList[0].name = "Root";
		loadtoAlbum (rModel.albumList[0], dir);

		for (int i = 0; i < dirs.Length; i++) {
			rModel.albumList[i+1] = new Album ();
			rModel.albumList[i+1].path = dirs[i].FullName;
			rModel.albumList[i+1].name = dirs[i].Name;
			loadtoAlbum (rModel.albumList[i+1], dirs[i]);
//			Debug.Log (rModel.albumList [i].name);
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
