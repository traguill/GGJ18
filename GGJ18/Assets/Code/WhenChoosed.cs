using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenChoosed : MonoBehaviour
{
    public Sprite when_selected;
    SpriteRenderer s_ren;

    void Start()
    {
        s_ren = GetComponent<SpriteRenderer>();
    }

    void OnPick()
    {
        s_ren.sprite = when_selected;
    }
}
