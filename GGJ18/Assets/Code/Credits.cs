using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	void Update () {
	    if(Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
