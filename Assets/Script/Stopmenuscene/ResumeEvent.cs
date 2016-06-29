using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ResumeEvent : MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    public GameObject resume;
    public GameObject replay;
    public GameObject quit;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        resume.SetActive(false);
        replay.SetActive(false);
        quit.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("TestClick");


        //throw new NotImplementedException();
    }

}
