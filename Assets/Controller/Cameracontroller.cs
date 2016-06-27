using UnityEngine;
using System.Collections;

public class Cameracontroller: MonoBehaviour {
    private Camera _mainCamera;

    public float UIEvalation;

    public Vector3 MenuButtonPosition;
    public Vector3 HealthBarPosition;
    public Vector3 ScoreBoardPosition;


    public GameObject ScoreBoardPrefab;
    public GameObject MenuButtonPrefab;
    public GameObject HealthBarPrefab;

    public GameObject ScoreBoardDebug;
    public GameObject MenuButtonDebug;
    public GameObject HealthBarDebug;



    void Start() {
        ScoreBoardDebug.SetActive(false);
        MenuButtonDebug.SetActive(false);
        HealthBarDebug.SetActive(false);
        _mainCamera = Camera.main;
        SetUpScene();
    }

	void Update () {
        //TODO:this need to be refactor onto buttons
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


    void SetUpScene() {

        Instantiate(ScoreBoardPrefab, _mainCamera.ViewportToWorldPoint(ScoreBoardPosition), Quaternion.identity);
        Instantiate(HealthBarPrefab, _mainCamera.ViewportToWorldPoint(HealthBarPosition), Quaternion.identity);
        Instantiate(MenuButtonPrefab, _mainCamera.ViewportToWorldPoint(MenuButtonPosition), Quaternion.identity);

    }

    public void DebugScene() {

        Camera debugCamera = this.GetComponent<Camera>();

        ScoreBoardDebug.transform.position = debugCamera.ViewportToWorldPoint(ScoreBoardPosition);
        HealthBarDebug.transform.position = debugCamera.ViewportToWorldPoint(HealthBarPosition);
        MenuButtonDebug.transform.position = debugCamera.ViewportToWorldPoint(MenuButtonPosition);
    }



}
