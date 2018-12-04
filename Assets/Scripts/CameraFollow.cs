using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    internal Quaternion _originalRotation;
    private Vector3 _cameraOffset;
    private Quaternion _lastBallRotation;
    private float _realVelocityUpdateThreshold;
    
    [Range(0.001f, 1.0f)]
    public float _velocityUpdateThreshold = 0.2f;
    [Range(0.001f, 10.0f)]
    public float _rotationSlerpSpeed = 0.67f;
    public GameObject _stageControllerObject;

    void Start()
    {
        _originalRotation = transform.rotation;
        _cameraOffset = transform.localPosition;
        _lastBallRotation = Quaternion.LookRotation(-_cameraOffset.normalized, Vector3.up);
        _realVelocityUpdateThreshold = _velocityUpdateThreshold * _velocityUpdateThreshold;
    }
    private void Update()
    {
        if (transform.parent?.gameObject == null)
            return;

        GameObject parentBall = transform.parent.gameObject;
        Rigidbody ballBody = parentBall.GetComponent<Rigidbody>();

        Vector3 viewDir;
        if (ballBody.velocity.sqrMagnitude > _realVelocityUpdateThreshold)
        {
            viewDir = ballBody.velocity;
        }
        else
        {
            //Reset camera to be level
            Vector3 camForward = transform.forward;
            camForward.y = 0.0f;
            viewDir = camForward;
        }

        //Create rotation to face the direction we want to face
        Quaternion rot = Quaternion.LookRotation(viewDir.normalized, Vector3.up);

        //Jumping to the velocity rotation is too jarring
        rot = Quaternion.Slerp(_lastBallRotation, rot, _rotationSlerpSpeed * Time.deltaTime);

        _lastBallRotation = rot;

        //rot = _stageControllerObject.transform.rotation * rot;

        Vector3 targetPosition = transform.parent.position + rot * _cameraOffset;
        Quaternion targetRotation = rot * _originalRotation;

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
