using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class AudioButton : MonoBehaviour,IPointerClickHandler
{
    public AudioSource music;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!music.isPlaying) {
            music.Play();
        }
        
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
