using System.Collections;
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
    private void Start()
    {
        tempo = LevelManager.current_level.action_time;
        grid_pos = Grid.RealWorldToGridPos(transform.position);
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
                direction = Vector2.zero;
        }
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
        float rotation = 0.0f;
        switch (dir)
        {
            case Signal.MoveDirection.UP:
                rotation = 0.0f;
                break;
            case Signal.MoveDirection.RIGHT:
                rotation = -90.0f;
                break;
            case Signal.MoveDirection.DOWN:
                rotation = 180.0f;
                break;
            case Signal.MoveDirection.LEFT:
                rotation = 90.0f;
                break;
        }
        direction = Quaternion.Euler(0.0f, 0.0f, rotation) * looking_at;

        transform.rotation *= Quaternion.Euler(0.0f, 0.0f, rotation);
        looking_at = direction;
        orders.RemoveAt(0);
        
        if (isValidGridPos(grid_pos + new Vector2(direction.x, direction.y)))
        {
            moving = true;
            StartCoroutine(MoveSmooth(transform.position + new Vector3(direction.x, direction.y) * Grid.current_grid.real_units));
            grid_pos += Grid.roundVec2(new Vector2(direction.x, direction.y));
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
