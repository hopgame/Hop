using UnityEngine;
using System.Collections;
using System.IO;

public class Initialize : MonoBehaviour {

    // Use this for initialization
    private string filetrack;
	void Start () {
        filetrack = System.Environment.CurrentDirectory;
        Debug.Log(filetrack);
        DirectoryInfo current = new DirectoryInfo(filetrack);
        DirectoryInfo[] descent;
        descent = current.GetDirectories();
        for(int i = 0; i < descent.Length; i++)
        {
            Debug.Log(descent[i].Name);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
