using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentCounter : MonoBehaviour
{
    public Text textBoxToEdit; 
    public static int presentNum; 
	
    void Start()
    {
        presentNum = 0;
    }

	// Update is called once per frame
	void Update ()
    {
        textBoxToEdit.text = "x " + presentNum.ToString();
	}
}
