using UnityEngine;

public class GrabCollectables : MonoBehaviour
{
    public float spinSpeed = 50f;
    public float heightOffset = 10.0f;

    public float amplitude = 0.2f;
    public float frequency = 0.4f;
    private float time = 0.0f;
    private float baseRotateTime = 0.0f;

    private bool grabbed = false;
    
    void Start()
    {
        time = Random.value;
        transform.RotateAround(transform.position, transform.parent.up, Random.value * 360.0f);
    }
	
	void Update()
    {
        //Quaternion deltaRot = Quaternion.AngleAxis(Time.deltaTime, transform.parent.up);
        //transform.rotation = transform.rotation * deltaRot;
        transform.RotateAround(transform.position, transform.parent.up, Time.deltaTime * spinSpeed); // rotate around a vertical axis

        time += Time.deltaTime;
        
        transform.position = Vector3.Lerp(
            transform.parent.position - transform.parent.up * (heightOffset * 0.5f),
            transform.parent.position + transform.parent.up * (heightOffset * 0.5f),
            (1.0f - Mathf.Cos(time * Mathf.PI)) * 0.5f);
    }
    
    void OnTriggerEnter(Collider impact)
    {
        if (!grabbed && impact.gameObject.tag.Equals("PlayerBall"))
        {
            grabbed = true;
            PresentCounter.presentNum += 1;
            AudioSource sound = GetComponent<AudioSource>();
            sound.Play();
            Renderer r = GetComponent<Renderer>();
            r.enabled = false;
            Destroy(gameObject, 1.0f);
        }
    }
}
