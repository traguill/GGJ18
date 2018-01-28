using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionMenu : MonoBehaviour {

    public string[] questions;
    Text label;
    int q_id = -1;
    string q_string;
    bool writting = false;
    float write_timer = 0.0f;

    int question_letter = 0;
    int question_size = 0;

    float write_max_time = 0.0f;

	// Use this for initialization
	void Start () {
        label = GetComponent<Text>();
        GetMaxTime();
	}

    void GetMaxTime()
    {
        write_max_time = Random.RandomRange(0.05f, 0.12f);
    }

    void Update()
    {
        if(writting)
        {
            write_timer += Time.deltaTime;
            if(write_timer > write_max_time)
            {
                question_letter++;
                if(question_letter > question_size)
                {
                    writting = false;
                    MainMenu.menu.monkey.anim.SetBool("talk", false);
                    MainMenu.menu.EndAsking();
                }
                else
                {
                    GetMaxTime();
                    label.text = q_string.Substring(0, question_letter);
                }
                write_timer = 0.0f;
            }
        }
    }

    public void AskQuestion(int id)
    {
        MainMenu.menu.monkey.anim.SetBool("talk", true);
        q_id = id;
        writting = true;
        q_string = questions[q_id];
        question_size = questions[q_id].Length;
        question_letter = -1;
    }


	
}
