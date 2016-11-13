using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{

    private GameObject[] doors;
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
            var sprite = d.transform.FindChild("ExitIcon").gameObject;
			if (sprite != null)
			{
				sprite.SetActive(false);
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
				if (hitInfo.distance < 10)
				{
					var sprite = hitObject.transform.FindChild("ExitIcon").gameObject;
					sprite.SetActive(true);
				}
            }
			if (hitObject.tag == "doors" && Input.GetKeyDown (KeyCode.Return)) {

				var j2s = hitObject.GetComponent<JumpToScene> ();
				SceneManager.LoadScene (j2s.scene);
			} else if (DataScan.OS == DataScan.OS_TYPE.MAC && Input.GetKeyDown ("joystick button 16")) {
				var j2s = hitObject.GetComponent<JumpToScene> ();
				SceneManager.LoadScene (j2s.scene);
			} else if (Input.GetKeyDown ("joystick button 0")) {
				var j2s = hitObject.GetComponent<JumpToScene> ();
				SceneManager.LoadScene (j2s.scene);
			}




        }


    }
}
