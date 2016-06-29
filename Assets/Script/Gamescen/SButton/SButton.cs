using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SButton : MonoBehaviour {

    public GameObject resume;
    public GameObject replay;
    public GameObject quit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Touched()
    {
        Time.timeScale = 0;
        resume.SetActive(true);
        replay.SetActive(true);
        quit.SetActive(true);

    }
}
