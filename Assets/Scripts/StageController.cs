using UnityEngine;

public class StageController : MonoBehaviour
{
    [Range(0.0f, 90.0f)]
    public float _maxPitchDeg = 30.0f;
    [Range(0.0f, 90.0f)]
    public float _maxRollDeg = 30.0f;
    [Range(0.01f, 200.0f)]
    public float _interpSpeed = 2.0f;
    public GameObject _playerBall;
    public Camera _camera;

    private Quaternion _targetRotation;

    void Start()
    {
        //No need to see the mouse
        Cursor.lockState = CursorLockMode.Locked;

        //The camera with the MainCamera tag will be used for relative rotations
        //_camera = Camera.main;
    }
	void FixedUpdate()
    {
        float forwardStrength = Input.GetKey(KeyCode.W) ? 1.0f : 0.0f;
        float backwardStrength = Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
        float rightStrength = Input.GetKey(KeyCode.D) ? 1.0f : 0.0f;
        float leftStrength = Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;

        float lr = leftStrength + rightStrength;
        float fb = forwardStrength + backwardStrength;
        
        float pitchDelta = fb * _maxPitchDeg;
        float rollDelta = -lr * _maxRollDeg;

        //Create delta rotations in camera local space
        Quaternion targetPitchRoll = Quaternion.Euler(pitchDelta, 0.0f, rollDelta);

        Vector3 camForward = _camera.transform.forward;
        camForward.y = 0.0f;
        camForward.Normalize();
        Quaternion camYaw = Quaternion.FromToRotation(Vector3.forward, camForward);
        
        _targetRotation = camYaw * targetPitchRoll;

        Quaternion rot = Quaternion.Slerp(transform.rotation, _targetRotation, Time.fixedDeltaTime * _interpSpeed);
        transform.position = _playerBall.transform.position + (rot * (-_playerBall.transform.position));
        transform.rotation = rot;
    }
}
