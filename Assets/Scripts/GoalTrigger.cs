using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    public float _countdownSeconds = -1.0f;
    public GameObject _playerBall;
    public Camera _camera;
    public float _flyAwayHeight = 200.0f;
    public float _flyAwaySpeed = 0.05f;
    public Text _countdownText;
    public string _onFailedSceneName;
    public string _onSuccessSceneName;

    private bool _finished = false;
    private float _flyAwayTime = 0.0f;
    private float _startY;
    private float _endY;
    private bool _stopTick = false;
    
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "PlayerBall")
            return;

        _playerBall.transform.DetachChildren();
        _finished = true;
        _flyAwayTime = 0.0f;
        _startY = _playerBall.transform.position.y;
        _endY = _playerBall.transform.position.y + _flyAwayHeight;

        var body = _playerBall.GetComponent<Rigidbody>();
        body.constraints |= RigidbodyConstraints.FreezePositionY;
        body.drag = 1.5f; //Make sure the ball comes to a stop, but smoothly
    }
    private void Update()
    {
        if (_stopTick)
            return;

        if (!_finished)
        {
            if (_countdownSeconds >= 0.0f)
            {
                _countdownSeconds -= Time.deltaTime;
                if (_countdownSeconds <= 0.0f)
                {
                    _countdownSeconds = 0.0f;
                    _stopTick = true;
                    if (!string.IsNullOrWhiteSpace(_onFailedSceneName))
                    {
                        Scene failed = SceneManager.GetSceneByName(_onFailedSceneName);
                        SceneManager.SetActiveScene(failed);
                    }
                    return;
                }
                _countdownText.text = TimeSpan.FromSeconds(_countdownSeconds).ToString("mm:ss");
            }
        }
        else
        {
            Vector3 pos = _playerBall.transform.position;
            float time = Mathf.Pow((_flyAwayTime += Time.deltaTime) * _flyAwaySpeed, 4.0f);
            if (time >= 1.0f)
            {
                _stopTick = true;
                if (!string.IsNullOrWhiteSpace(_onSuccessSceneName))
                {
                    Scene success = SceneManager.GetSceneByName(_onSuccessSceneName);
                    SceneManager.SetActiveScene(success);
                }
                return;
            }
            pos.y = _startY + (_endY - _startY) * time;
            _playerBall.transform.position = pos;
            _camera.transform.rotation = Quaternion.LookRotation(_playerBall.transform.position - _camera.transform.position, Vector3.up);
        }
    }
}
