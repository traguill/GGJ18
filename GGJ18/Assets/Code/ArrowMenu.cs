﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowMenu : MonoBehaviour
{

    public Sprite UpArrow;
    public Sprite DownArrow;
    public Sprite LeftArrow;
    public Sprite RightArrow;

    public GameObject arrow_img;

    List<Animator> arrows;
    List<Image> arrows_img;

    public Signal signal;
    public Player player;

    public float alfa_choos = 0.8f;
    public float alfa_crepe = 0.5f;

    void Start()
    {
        arrows = new List<Animator>();
        arrows_img = new List<Image>();
        for (int i = 0; i < signal.max_orders; i++)
        {
            GameObject go = Instantiate(arrow_img, transform);
            arrows.Add(go.GetComponent<Animator>());
            arrows_img.Add(go.GetComponent<Image>());
        }
    }

    void Update()
    {
        List<Signal.MoveDirection> current_movements;

        if (player != null)
            current_movements = player.GetOrders();
        else
            current_movements = signal.GetOrders();


        for (int i = 0; i < signal.max_orders; i++)
        {
            if(current_movements.Count > i)
            {
                arrows_img[i].color = new Color(1f,1f,1f,alfa_choos);
                switch (current_movements[i])
                {
                    case Signal.MoveDirection.UP:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("up", true);
                        break;
                    case Signal.MoveDirection.RIGHT:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("left", true);
                        arrows[i].transform.localScale = new Vector3(-1,1,1);
                        break;
                    case Signal.MoveDirection.WAIT:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("wait", true);
                        break;
                    case Signal.MoveDirection.LEFT:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("left", true);
                        break;
                }
            }
            else
            {
                arrows_img[i].color = new Color(1f, 1f, 1f, alfa_crepe);
                arrows[i].transform.localScale = Vector3.one;
                arrows[i].SetBool("up", false);
                arrows[i].SetBool("wait", false);
                arrows[i].SetBool("left", false);
            }
        }
    }

    void DeactivateOthers(Animator anim)
    {
       anim.SetBool("up", false);
        anim.SetBool("wait", false);
        anim.SetBool("left", false);
    }
}