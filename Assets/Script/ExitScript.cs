using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.root.gameObject;
            if(hitObject.tag == "Door" && Input.GetKeyDown(KeyCode.Return))
            {
                var j2s = hitObject.GetComponent<JumpToScene>();
                SceneManager.LoadScene(j2s.scene);
            }
            

            

        }


    }
}
