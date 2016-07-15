using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    // Use this for initialization
    public static int currentscore = 0;
    public static int currentcombo = 0;

	void Start () {
	
	}
    public void addScore(int incre)
    {
        currentscore += incre;
    }
    public void addCombo()
    {
        currentcombo++;
    }
    public void clearcombo()
    {
        currentcombo = 0;
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
