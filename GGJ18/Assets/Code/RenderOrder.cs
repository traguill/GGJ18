using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour {

    SpriteRenderer s_ren;
	void Start ()
    {
        s_ren = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        s_ren.sortingOrder = -Mathf.RoundToInt(transform.position.y);
	}
}
