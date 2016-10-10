using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{

    private GameObject[] doors;
    // Use this for initialization
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject d in doors)
        {
            var sprite = d.transform.FindChild("ExitIcon").gameObject;
            sprite.SetActive(false);
        }
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.root.gameObject;
            if (hitObject.tag == "Door")
            {
                var sprite = hitObject.transform.FindChild("ExitIcon").gameObject;
                sprite.SetActive(true);
            }
            if (hitObject.tag == "Door" && Input.GetKeyDown(KeyCode.Return))
            {

                var j2s = hitObject.GetComponent<JumpToScene>();
                SceneManager.LoadScene(j2s.scene);
            }




        }


    }
}
