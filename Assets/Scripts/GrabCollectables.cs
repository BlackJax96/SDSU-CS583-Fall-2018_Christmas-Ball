using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollectables : MonoBehaviour {

    private GameObject present;
    private GameObject axis; 
    public float spinSpeed;

    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 tmp = new Vector3();
    Vector3 postOffset = new Vector3();

    
    void Start () {
        present = GameObject.FindGameObjectWithTag("PresentCollectable");
        axis = GameObject.Find("CollectableAxis");
        postOffset = transform.position;
	}
	
	void Update () {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.RotateAround(axis.transform.position, Vector3.up, Time.deltaTime * spinSpeed); // rotate around a vertical axis
        tmp = postOffset;
        tmp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tmp;

    }
}
