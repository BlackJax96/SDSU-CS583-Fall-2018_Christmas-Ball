using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollectables : MonoBehaviour {
    
    public GameObject present;
    public GameObject axis;
    public float spinSpeed = 50f;

    public float amplitude = 0.2f;
    public float frequency = 0.4f;
    //Vector3 tmp = new Vector3();
    //Vector3 postOffset = new Vector3();

    
    void Start () {
        //postOffset = transform.position;
	}
	
	void Update () {
        if(present != null) // if the present hasn't been destroyed yet
        {
            present.transform.RotateAround(axis.transform.position, Vector3.up, Time.deltaTime * spinSpeed); // rotate around a vertical axis
        }
            
        /*
        tmp = postOffset;
        tmp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;  // levitating up and down
        transform.position = tmp;
        */
    }
    
    void OnTriggerEnter(Collider impact)
    {
        if (impact.gameObject.tag.Equals("PlayerBall"))
        {
            Destroy(present);
            PresentCounter.presentNum += 1;
        }
    }
}
