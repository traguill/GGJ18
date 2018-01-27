using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour 
{
    public static FadeInOut fade;

    Image img;

    Color black = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    bool fade_in = false;
    bool fade_out = false;

    float value = 0.0f;
    public float fade_in_speed = 8.0f;
    public float fade_out_speed = 6.0f;

    string scene_name;

    public bool start_fadeout = false;

    void Awake()
    {
        fade = this;
        img = GetComponent<Image>();
        if (start_fadeout)
            FadeOut();
    }

    void Update()
    {
        if(fade_in)
        {
            value += Time.deltaTime * 3.0f;
            if(value >= 1.0f)
            {
                value = 1.0f;
                fade_in = false;
                SceneManager.LoadScene(scene_name);
            }
            img.color = new Color(value, value, value, value);
        }

        if(fade_out)
        {
            value -= Time.deltaTime * fade_out_speed;
            if(value <= 0.0f)
            {
                fade_out = false;
                value = 0.0f;
            }
            img.color = new Color(1.0f, 1.0f, 1.0f, value);
        }
    }

    public void FadeIn(string scene_to_launch)
    {
        img.enabled = true;
        img.color = black;
        fade_in = true;
        value = 0.0f;
        scene_name = scene_to_launch;
    }

    public void FadeOut()
    {
        img.enabled = true;
        img.color = white;
        fade_out = true;
        value = 1.0f;
    }
}
