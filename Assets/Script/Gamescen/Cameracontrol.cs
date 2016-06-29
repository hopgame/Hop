using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cameracontrol: MonoBehaviour {

	// Use this for initializatio
	void start()
    {
        int ManualWidth = 960;
        int ManualHeight = 640;
        int manualHeight;
        if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
			manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
        else
            manualHeight = ManualHeight;
        Camera camera = GetComponent<Camera>();
        float scale = System.Convert.ToSingle(manualHeight / 640f);
        camera.fieldOfView *= scale;
    }
	// Update is called once per frame
	void FixedUpdate () {
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
