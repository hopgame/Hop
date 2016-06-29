using UnityEngine;
using System.Collections;

public class Topscrpit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private Rigidbody rb;
    //private GameObject hp;
    private ConstantForce cf;
    void OnCollisionEnter(Collision collisionInfo)
    {
        // Debug.Log("Hiting something");
        if (collisionInfo.gameObject.tag.CompareTo("Hopper") == 0)
        {
            rb = collisionInfo.rigidbody;
            rb.useGravity = true;
            Vector3 vertical = new Vector3(0.0f, 0.0f, 0.0f);
            //rb.velocity = vertical;
            //hp = collisionInfo.gameObject;
            cf = rb.GetComponent<ConstantForce>();
            cf.force = vertical;

            //Debug.Log("Finish the process");
        }
    }
}
