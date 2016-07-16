using UnityEngine;
using System.Collections;


public class Tbutton : MonoBehaviour {
    public Hopperscript hopper;
    public GameController Gamecontroller;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void touched()
    {
        if (hopper.ifperfect() == 1)//user touch the button when the beats only heats the Hopper
        {
            hopper.addGScore();//add good score
            Gamecontroller.addCombo();//add one combo
            hopper.currentbeats = hopper.beatsqueue.Dequeue(); //dequeue the beats which has already been played
            hopper.currentbeats.gameObject.SetActive(false);//set the beat inactive
        }
        if(hopper.ifperfect() == 2)//user touch the button when the beats heats the center
        {
            hopper.addPScore();//add perfect score
            Gamecontroller.addCombo();//add one combo
            hopper.currentbeats = hopper.beatsqueue.Dequeue();//dequeue the beats which has already been played
            hopper.currentbeats.gameObject.SetActive(false);//set the beat inactive
        }


    }
}
