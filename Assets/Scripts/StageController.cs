using UnityEngine;

public class StageController : MonoBehaviour
{
    [Range(0.0f, 90.0f)]
    public float _maxPitchDeg = 45.0f;
    [Range(0.0f, 90.0f)]
    public float _maxRollDeg = 45.0f;
    [Range(0.01f, 200.0f)]
    public float _movementStrength = 0.09f;
    
    private Camera _camera;

    void Start()
    {
        //No need to see the mouse
        Cursor.lockState = CursorLockMode.Locked;

        //The camera with the MainCamera tag will be used for relative rotations
        _camera = Camera.main;
    }
	void FixedUpdate()
    {
        //Retrieve mouse movement
        Vector2 mouseDelta = new Vector2(
            Input.GetAxis("Mouse X") * Time.fixedDeltaTime * _movementStrength,
            Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * _movementStrength);

        float pitchDelta = mouseDelta.y;
        float rollDelta = -mouseDelta.x;

        //Only do anything if the mouse has moved enough
        if (Mathf.Abs(pitchDelta) + Mathf.Abs(rollDelta) > 0.01f)
        {
            //Create delta rotations in camera local space
            Quaternion delta = Quaternion.Euler(pitchDelta, 0.0f, rollDelta);

            //Rotate local up vector by delta
            //We use the up direction because up is affected by both pitch and roll.
            Vector3 localUpVec = delta * Vector3.up;

            //Transform camera local up delta vector to world space up delta vector
            Vector3 worldUpVec = _camera.transform.TransformDirection(localUpVec);

            //Create rotation from real camera world up vector to calculated world up vector
            Quaternion worldDeltaRot = Quaternion.FromToRotation(_camera.transform.up, worldUpVec);

            //Apply new world delta rotation to the current world rotation
            Quaternion rotation = transform.rotation * worldDeltaRot;

            //TODO: clamp rotation using max pitch and roll deg vars
            transform.rotation = rotation;
        }
    }
}
