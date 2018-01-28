using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : MonoBehaviour 
{

    public Animator wheels;
    public Animator engine;
    //Moving -> wheels + engine + translation
    //Stop with engine on -> engine
    //Stop -> nothing
    public float speed = 2.0f;
    public Transform exit_point;
    public Transform stop_point;
    public float engine_time_delay = 1.0f;

    Vector3 destination;
    bool move = false;
    bool engine_stops_delay = false;

    public bool van_enter = false;
    public string next_scene_name;

    public Transform player_spawn_position;

    AudioSource source;
    public AudioClip van_turn_on;
    public AudioClip van_turn_off;


    void Start()
    {
        source = GetComponent<AudioSource>();
        if(van_enter)
            EnterVan();
    }

    void Update()
    {
        if(move)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            if(Mathf.Abs(destination.x - transform.position.x) <= 3)
            {
                move = false;
                wheels.speed = 0.0f;
                if (engine_stops_delay)
                    StartCoroutine("TurnOffEngine");
            }
        }
    }

    public void EnterVan()
    {
        source.PlayOneShot(van_turn_on);
        destination = stop_point.position;
        move = true;
        wheels.speed = 1.0f;
        engine_stops_delay = true;
        engine.SetBool("on", true);
    }

    public void ExitVan()
    {
        source.Stop();
        destination = exit_point.position;
        source.PlayOneShot(van_turn_on);
        move = true;
        wheels.speed = 1.0f;
        engine.SetBool("on", true);
    }

    IEnumerator TurnOffEngine()
    {
        source.PlayOneShot(van_turn_off);
        yield return new WaitForSeconds(engine_time_delay);
        engine.SetBool("on", false);
        engine_stops_delay = false;
        yield return new WaitForSeconds(4.0f);
        FadeInOut.fade.FadeIn(next_scene_name);
        yield return 0;
    }
}
