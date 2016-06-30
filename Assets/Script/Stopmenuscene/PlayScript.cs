using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PlayScript : MonoBehaviour,IPointerClickHandler{

    // Use this for initialization
    public GameObject SelectSong;
    public GameObject self;
    public GameObject Mainmenu;
    public GameObject Option;
    void Start () {
           self.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SelectSong.SetActive(true);
        Mainmenu.SetActive(true);
        self.SetActive(false);
        Option.SetActive(false);
        //quit.SetActive(false);
        //Time.timeScale = 1;
        Debug.Log("NextMenu");


        //throw new NotImplementedException();
    }
}
