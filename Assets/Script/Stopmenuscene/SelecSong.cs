using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;


public class SelecSong : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Selectsong");
        Debug.Log("Switch to selecmenu");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
