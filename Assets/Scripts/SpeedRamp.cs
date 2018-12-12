using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour
{
    public float power;
    public Rigidbody rigidBody;
    private GameObject ball;

	void Start ()
    {
        ball = GameObject.FindGameObjectWithTag("PlayerBall");
        rigidBody = ball. GetComponent<Rigidbody>();
    }
	
    void OnTriggerEnter(Collider impact)
    {
        if (impact.CompareTag("PlayerBall"))
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(transform.forward * power);
        }
    }
 

}
