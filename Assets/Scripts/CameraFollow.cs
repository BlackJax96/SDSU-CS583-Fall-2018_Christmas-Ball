using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Quaternion _originalRotation;
    private Vector3 _cameraOffset;
    private Quaternion _lastBallDir;
    private float _realVelocityUpdateThreshold;

    [Range(0.001f, 1.0f)]
    public float _velocityUpdateThreshold = 0.2f;
    [Range(0.001f, 10.0f)]
    public float _rotationSlerpSpeed = 0.67f;

    void Start ()
    {
        _originalRotation = transform.rotation;
        _cameraOffset = transform.localPosition;
        _lastBallDir = Quaternion.LookRotation(-_cameraOffset.normalized, Vector3.up);
        _realVelocityUpdateThreshold = _velocityUpdateThreshold * _velocityUpdateThreshold;
    }
    private void Update()
    {
        GameObject parentBall = transform.parent.gameObject;
        Rigidbody ballBody = parentBall.GetComponent<Rigidbody>();
        Vector3 velocity = ballBody.velocity;
        Quaternion dir = _lastBallDir;

        //Can't call Quaternion.LookRotation on a zero vector (when not moving, velocity is 0)
        if (velocity.sqrMagnitude > _realVelocityUpdateThreshold)
        {
            Vector3 vel = velocity.normalized;

            //Create rotation to face the direction the ball is moving
            dir = Quaternion.LookRotation(vel, Vector3.up);

            //Jumping to the velocity rotation is too jarring
            dir = Quaternion.Slerp(_lastBallDir, dir, _rotationSlerpSpeed * Time.deltaTime);

            _lastBallDir = dir;
        }

        Vector3 targetPosition = transform.parent.position + dir * _cameraOffset;
        Quaternion targetRotation = dir * _originalRotation;

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
