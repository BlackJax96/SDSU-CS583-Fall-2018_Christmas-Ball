using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject _playerBall;
    public Camera _camera;
    public float _flyAwayHeight = 200.0f;
    public float _flyAwaySpeed = 0.05f;

    private bool _finished = false;
    private float _flyAwayTime = 0.0f;
    private float _startY;
    private float _endY;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Finish")
            return;

        print("Finished stage");

        _playerBall.transform.DetachChildren();
        _finished = true;
        _flyAwayTime = 0.0f;
        _startY = _playerBall.transform.position.y;
        _endY = _playerBall.transform.position.y + _flyAwayHeight;

        var body = _playerBall.GetComponent<Rigidbody>();
        body.constraints |= RigidbodyConstraints.FreezePositionY;
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private float Lerp(float start, float end, float time)
    {
        return start + (end - start) * time;
    }
    private const float E = 2.7182818284590451f;
    public static float BounceTimeModifier(float time, int bounces = 4, float bounceFalloff = 4.0f)
    {
        return 1.0f - Mathf.Pow(E, -bounceFalloff * time) * Mathf.Abs(Mathf.Cos(Mathf.PI * (0.5f + bounces) * time));
    }
    private float FlyAway()
    {
        _flyAwayTime += Time.deltaTime;
        return Lerp(_startY, _endY, BounceTimeModifier(_flyAwayTime * _flyAwaySpeed));
    }

    private void Update()
    {
        if (!_finished)
            return;

        Vector3 pos = _playerBall.transform.position;
        pos.y = FlyAway();
        _playerBall.transform.position = pos;
        _camera.transform.rotation = Quaternion.LookRotation(_playerBall.transform.position - _camera.transform.position, Vector3.up);
    }
}
