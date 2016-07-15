using UnityEngine;
using System.Collections;



public class CenterScript : MonoBehaviour {
    public Hopperscript hopper;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider beats)
    {
        hopper.perfectvalue = 2;//now if the user touch the button it will be a perfect

    }
    void onTriggerExit(Collider beats)
    {
        hopper.perfectvalue = 1;
    }
}
