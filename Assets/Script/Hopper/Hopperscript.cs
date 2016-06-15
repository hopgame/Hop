using UnityEngine;
using System.Collections;

public class Hopperscript : MonoBehaviour {
    public Rigidbody rb;
    private bool up = false;
    void start()
    {
        rb = this.GetComponent<Rigidbody>();
       
    }
    void update(){

           
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Hiting something");
        if (collisionInfo.gameObject.tag.CompareTo("Beatspad") == 0)
        {
            Debug.Log("Hit the pad");
            rb.useGravity = false;
            Vector3 vertical = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(vertical);
        }
        if (collisionInfo.gameObject.tag.CompareTo("Top") == 0)
        {
            rb.useGravity = true;

        }
        
    }
}
