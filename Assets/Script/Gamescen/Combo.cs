using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Combo : MonoBehaviour {
    public GameController Gamecontroller;
    private int combonumber;    // Use this for initialization
    private Text text;
    void Start () {
        text = this.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        combonumber = Gamecontroller.currentcombo;
        text.text = combonumber.ToString();
        
	}
}