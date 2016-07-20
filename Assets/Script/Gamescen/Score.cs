using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public GameController Gamecontroller;
    private int score;    // Use this for initialization
    private Text text;
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score = Gamecontroller.currentscore;
        text.text = score.ToString();

    }
}
