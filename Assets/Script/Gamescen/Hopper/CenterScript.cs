using UnityEngine;
using System.Collections;



public class CenterScript : MonoBehaviour {
    public Hopperscript hopper;//father hopper
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider beats)
    {
        hopper.entering--;
        if (hopper.occupy == 0)//test if the previous beat still in the hopper
        {
            hopper.perfectvalue = 2;//now if the user touch the button it will be a perfect
        }

    }
    void OnTriggerStay(Collider beats)
    {
        if(hopper.occupy == 0)//test if the previous beat still in the hopper
        {
            hopper.perfectvalue = 2;//now if the user touch the button it will be a perfect
        }
        

    }
    void onTriggerExit(Collider beats)
    {
        hopper.perfectvalue = 1;
        hopper.occupy++;//add one reamin beat in hopper

    }
}
