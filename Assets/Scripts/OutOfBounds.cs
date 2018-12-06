using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour {

    private GameObject ballObject;
    private string currentScene;
    public float yValueToRestart;
   
    void Start()
    {
        ballObject = GameObject.FindGameObjectWithTag("PlayerBall");
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if(ballObject.transform.position.y < yValueToRestart)
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
