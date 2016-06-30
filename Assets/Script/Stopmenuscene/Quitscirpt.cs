using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
public class Quitscirpt : MonoBehaviour,IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Welcome");
        Debug.Log("Quiting the game");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
