using UnityEngine;
using System.Collections;

public class Cameracontrol: MonoBehaviour {

	// Use this for initializatio
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag.CompareTo("Tbutton") == 0)
                    {
                        hit.transform.gameObject.SendMessage("Touched");
                    }
                    if (hit.transform.gameObject.tag.CompareTo("Sbutton") == 0)
                    {
                        hit.transform.gameObject.SendMessage("Touched");
                    }
                }
            }
        }
    }
}
