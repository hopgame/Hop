using UnityEngine;
using System.Collections;

public class Hopperscript : MonoBehaviour {
    private int scoreforround = 0;//score for one round;
    private int goodincre = 10;//socre to add for good
    private int perfectincre = 20;//score to add for perfect
    public int perfectvalue = 0;//for button to determine if the hopper is triggering beats(good or perfect)
    public GameController tester;//the controller
    public Collider currentbeats;//the current beats

    void start()
    {
       
    }
    void update(){

           
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Hiting something");
        if (collisionInfo.gameObject.tag.CompareTo("Beatspad") == 0)//push the score to controller when hopper hit the pad
        {
            tester.addScore(scoreforround);//add the current score in the hopper into controller to refreash the score board
        }
        
    }
    void  OnTriggerEnter(Collider beats)
    {
        perfectvalue = 1;//if user touch the button now will get a good
        currentbeats = beats;//this beats is ready fo setactive(false)

    }

    void OnTriggerExit(Collider beats)
    {
        perfectvalue = 0;//reset the jud value
        tester.clearcombo();//clear the combo in controller
        beats.gameObject.SetActive(false);// mark one miss, destory the beats

    }
    public int ifperfect()
    {
        return perfectvalue; //return the perfectvalue
    }
    public void addGScore()
    {
        scoreforround += goodincre;//add good score into the score of this round    
    }
    public void addPScore()
    {
        scoreforround += perfectincre;//add perfect score into the score of this round
    }
}
