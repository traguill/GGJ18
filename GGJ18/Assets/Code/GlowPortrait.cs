using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowPortrait : MonoBehaviour 
{
    Image render;

    public float flick_time = 0.2f;
    float timer = 0.0f;

    void Start()
    {
        render = GetComponent<Image>();
    }

	void Update () 
    {
        timer += Time.deltaTime;
        if(timer >= flick_time)
        {
            timer -= flick_time;
            render.enabled = !render.enabled;
        }
	}
}
