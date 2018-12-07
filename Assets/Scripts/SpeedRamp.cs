using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour {

    public float power;
    public Rigidbody rigidBody;
    private GameObject ball;

	void Start ()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        ball = GameObject.FindGameObjectWithTag("PlayerBall");
        rigidBody = ball. GetComponent<Rigidbody>();
    }
	
    void OnTriggerEnter(Collider impact)
    {
        if (impact.CompareTag("PlayerBall"))
        {
            rigidBody.AddForce(transform.forward * power);
        }
    }
 

}
