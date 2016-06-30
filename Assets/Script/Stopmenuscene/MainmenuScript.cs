using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class MainmenuScript : MonoBehaviour,IPointerClickHandler{

    // Use this for initialization
    public GameObject SelectSong;
    public GameObject self;
    public GameObject Play;
    public GameObject option;
    void Start () {
	       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectSong.SetActive(false);
        self.SetActive(false);
        Play.SetActive(true);
        option.SetActive(true);
        //throw new NotImplementedException();
    }
}
