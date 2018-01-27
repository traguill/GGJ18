﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public float tempo = 2.0f; //Change this to game tempo
    public float max_movement_time = 0.5f;
    float timer = 0.0f;
    List<Signal.MoveDirection> orders = new List<Signal.MoveDirection>();

    Vector2 direction = new Vector2();
    Vector2 looking_at = Vector2.up;
    public float mov_speed = 1.0f;
    Vector2 grid_pos;
    bool moving = false;
    Animator anim;
    SpriteRenderer s_ren;

    GameObject ruby;
    GameObject endposition;

    bool got_ruby = false;

    private void Start()
    {
        tempo = LevelManager.current_level.action_time;
        grid_pos = Grid.RealWorldToGridPos(transform.position);
        anim = GetComponent<Animator>();
        s_ren = GetComponent<SpriteRenderer>();

        ruby = GameObject.FindGameObjectWithTag("Item");
        endposition = GameObject.FindGameObjectWithTag("Finish");
    }

    void CheckWinAndRuby()
    {
        if(!got_ruby)
        {
            if(Vector3.Distance(ruby.transform.position,transform.position)< Grid.current_grid.real_units/2)
            {
                got_ruby = true;
                ruby.SetActive(false);
            }
                
        }
        else
        {
            if (Vector3.Distance(endposition.transform.position, transform.position) < 1f)
            {
                LevelManager.current_level.WinGame();
            }
        }
    }

    void Update () 
    {
        timer += Time.deltaTime;
        if(timer >= tempo && moving == false)
        {
            timer -= tempo;
            
            if (orders.Count != 0)
            {
                SetDirection(orders[0]);
                
            }
            else
            {
                direction = Vector2.zero;
                anim.SetBool("walk", false);
            }
        }

        s_ren.sortingOrder = -(int)grid_pos.y;
        CheckWinAndRuby();
    }

    public void ReceiveOrders(List<Signal.MoveDirection> data)
    {
        orders.AddRange(data);
    }

    public List<Signal.MoveDirection> GetOrders()
    {
        return orders;
    }

    public Vector2 GetPosInGrid()
    {
        return grid_pos;
    }

    void SetDirection(Signal.MoveDirection dir)
    {
        direction = looking_at;
        switch (dir)
        {
            case Signal.MoveDirection.UP:
                //Nothing go straight
                break;
            case Signal.MoveDirection.RIGHT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, -90.0f) * looking_at;
                anim.SetBool("turn_right", true);
                direction = Quaternion.Euler(0.0f, 0.0f, -90.0f) * direction;
                break;
            case Signal.MoveDirection.WAIT:
                direction = Vector2.zero;
                break;
            case Signal.MoveDirection.LEFT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, 90.0f) * looking_at;
                anim.SetBool("turn_left", true);
                direction = Quaternion.Euler(0.0f, 0.0f, 90.0f) * direction;
                break;
        }
        orders.RemoveAt(0);
        
        if (direction != Vector2.zero && isValidGridPos(grid_pos + new Vector2(direction.x, direction.y)))
        {
            moving = true;
            anim.SetBool("walk", true);
            StartCoroutine(MoveSmooth(transform.position + new Vector3(direction.x, direction.y) * Grid.current_grid.real_units));
            grid_pos += Grid.roundVec2(new Vector2(direction.x, direction.y));
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    IEnumerator MoveSmooth(Vector3 pos)
    {
        float current_move_time = 0f;
        while (max_movement_time > current_move_time)
        {
            transform.position = Vector3.Lerp(transform.position, pos, mov_speed * 0.02f);
            current_move_time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        moving = false;
        transform.position = pos;
    }

    bool isValidGridPos(Vector2 new_pos)
    {
        Vector2 v = Grid.roundVec2(new_pos);

        // Not inside Border?
        if (!Grid.insideBorder(v))
            return false;

        if (!Grid.PositionFree(v))
        {
            return false;
        }

        return true;
    }
}
