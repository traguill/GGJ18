using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitBehavior : MonoBehaviour 
{
    public float time_for_action = 1.0f;

    Animator anim;

    float speak_time = 0.0f;

    float trigger_glasses_time = 0.0f;

    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(speak_time != 0.0f)
        {
            speak_time -= Time.deltaTime;
            if(speak_time <= 0.0f)
            {
                speak_time = 0.0f;
                anim.SetBool("talk", false);
            }
        }
        else
        {
            trigger_glasses_time -= Time.deltaTime;
            if(trigger_glasses_time < 0.0f)
            {
                anim.Play("portrait_glasses");
                GetTriggerGlassesTime();
            }
        }
	}

    void GetTriggerGlassesTime()
    {
        trigger_glasses_time = Random.Range(1.0f, 5.0f);
    }

    public void Talk(int num_actions)
    {
        speak_time = num_actions * time_for_action;
        anim.SetBool("talk", true);
    }
}
