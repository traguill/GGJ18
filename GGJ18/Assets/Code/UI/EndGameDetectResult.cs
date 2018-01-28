using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameDetectResult : MonoBehaviour {

    Text txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();

        if(txt)
        {
            if(LevelManager.current_level)
            {
                if (LevelManager.current_level.won)
                    txt.text = "Win";
                else
                    txt.text = "Lose";
            }
        }
	}

}
