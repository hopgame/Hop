using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    // Use this for initialization
    public Hopperscript hopper0;
    public Hopperscript hopper1;
    public Hopperscript hopper2;
    public Hopperscript hopper3;
    public  int currentscore = 0;//score for now
    public  int currentcombo = 0;//combo for now
    public int goodincre = 10;//socre to add for good
    public int perfectincre = 20;//score to add for perfect
    

    void Start () {
	
	}
    public void addScore(int incre)//add score from each hopper to the controller
    {
        currentscore += incre;
    }
    public void addCombo()
    {
        currentcombo++;
    }
    public void clearcombo()//called when miss happened
    {
        currentcombo = 0;
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        if(currentcombo > 0 && currentcombo %50 == 0)
        {
            goodincre *= 2;
            perfectincre *= 2;
            
        }
        if(currentcombo == 0)
        {
            goodincre = 10;
            perfectincre = 20;
        }
	
	}
}
