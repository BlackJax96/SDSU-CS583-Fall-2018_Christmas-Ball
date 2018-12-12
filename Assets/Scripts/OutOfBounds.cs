using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    private GameObject ballObject;
    private string targetScene;
    public float yValueToRestart;
    public string sceneToLoad;

    void Start()
    {
        ballObject = GameObject.FindGameObjectWithTag("PlayerBall");
        if (string.IsNullOrWhiteSpace(sceneToLoad))
            targetScene = SceneManager.GetActiveScene().name;
        else
            targetScene = sceneToLoad;
    }

    void Update()
    {
        if(ballObject.transform.position.y < yValueToRestart)
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
