using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPortrait : MonoBehaviour {

    float timer = 0.0f;
    float wink_time = 0.0f;
    [HideInInspector]public Animator anim;
    bool talking = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        wink_time = Random.RandomRange(3.0f, 7.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!talking)
        {
            timer += Time.deltaTime;
            if(timer >= wink_time)
            {
                anim.Play("wink");
                timer -= wink_time;
            }
        }
	}
}
