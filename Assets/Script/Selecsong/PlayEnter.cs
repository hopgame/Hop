using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class PlayEnter : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Game-1");
        Debug.Log("Playing now");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
