using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{

    private GameObject[] doors;
	public float distance = 10;
    // Use this for initialization
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("doors");
    }

    // Update is called once per frame
    void Update()
    {
		if(doors.Length == 0)
		{
			doors = GameObject.FindGameObjectsWithTag("doors");
		}
        foreach (GameObject d in doors)
        {
            var spriteObject = d.transform.FindChild("ExitIcon");
            var labelObject = d.transform.FindChild("Label");
			GameObject sprite;
			GameObject label;
			if (spriteObject != null) {
				sprite = spriteObject.gameObject;
				if (sprite != null)
				{
					sprite.SetActive(false);
				}
			}
			if (labelObject != null) {
				label = labelObject.gameObject;
				if (label != null)
				{
					label.SetActive(false);
				}
			}



        }
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.root.gameObject;
            if (hitObject.tag == "doors")
            {
                if (hitInfo.distance < distance)
                {
                    var spriteObject = hitObject.transform.FindChild("ExitIcon");
                    var labelObject = hitObject.transform.FindChild("Label");
                    GameObject sprite;
                    GameObject label;
                    if (spriteObject != null)
                    {
                        sprite = spriteObject.gameObject;
                        if (sprite != null)
                        {
                            sprite.SetActive(true);
                        }
                    }
                    if (labelObject != null)
                    {
                        label = labelObject.gameObject;
                        if (label != null)
                        {
                            label.SetActive(true);
                        }
                    }
                }
            }
            if (hitObject.tag == "doors") { 
                var doorInfo = hitObject.GetComponent<DoorInfo>();
                
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (doorInfo != null)
                    {
                        DataScan.currentAlbum = DataScan.rootModel.albumList[doorInfo.albumIndex];
                    }
                    var j2s = hitObject.GetComponent<JumpToScene>();
                    SceneManager.LoadScene(j2s.scene);
                }
                else if (DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown("joystick button 16"))
                {
                    if (doorInfo != null)
                    {
                        DataScan.currentAlbum = DataScan.rootModel.albumList[doorInfo.albumIndex];
                    }
                    var j2s = hitObject.GetComponent<JumpToScene>();
                    SceneManager.LoadScene(j2s.scene);
                }
                else if (Input.GetKeyDown("joystick button 0"))
                {
                    if (doorInfo != null)
                    {
                        DataScan.currentAlbum = DataScan.rootModel.albumList[doorInfo.albumIndex];
                    }
                    var j2s = hitObject.GetComponent<JumpToScene>();
                    SceneManager.LoadScene(j2s.scene);
                }
            }




        }


    }
}
