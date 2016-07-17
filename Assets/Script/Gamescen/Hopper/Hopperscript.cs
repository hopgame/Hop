using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hopperscript : MonoBehaviour {
    private int scoreforround = 0;//score for one round;
    public int perfectvalue = 0;//for button to determine if the hopper is triggering beats(good or perfect)
    public GameController gamecontroller;//the controller
    public Collider currentbeats;//the current beats which need to be setactive false
    public Queue <Collider>beatsqueue = new Queue<Collider>();//queue for the beats entering the hopper
    public int occupy = 0;//the number of current beat leaving the center but still in the hopper
    public int entering = 0;//the number of current beat entering hopper but not yet entering center

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
            gamecontroller.addScore(scoreforround);//add the current score in the hopper into controller to refreash the score board
        }
        
    }
    void  OnTriggerEnter(Collider beats)
    {
        perfectvalue = 1;//if user touch the button now will get a good
        beatsqueue.Enqueue(beats);
        entering++;
    }

    void OnTriggerExit(Collider beats)
    {
        if(entering != 0 && occupy !=0 && perfectvalue != 2)
        {
            perfectvalue = 0;//reset the perfect value
        }
        gamecontroller.clearcombo();//clear the combo in controller
		occupy--;//minus one beats beacuase of leaving
        currentbeats = beatsqueue.Dequeue();//deqeue the exiting beat
        currentbeats.gameObject.SetActive(false);// mark one miss, destory the beat

    }
    public int ifperfect()
    {
        return perfectvalue; //return the perfectvalue
    }
    public void addGScore()
    {
        scoreforround += gamecontroller.goodincre;//add good score into the score of this round    
    }
    public void addPScore()
    {
        scoreforround += gamecontroller.perfectincre;//add perfect score into the score of this round
    }
}
