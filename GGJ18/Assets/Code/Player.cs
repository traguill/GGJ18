using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public float tempo = 2.0f; //Change this to game tempo
    float timer = 0.0f;
    List<Signal.MoveDirection> orders = new List<Signal.MoveDirection>();

    Vector2 direction = new Vector2();
    Vector2 looking_at = Vector2.up;
    public float mov_speed = 1.0f;

	void Update () 
    {
        timer += Time.deltaTime;
        if(timer >= tempo)
        {
            timer -= tempo;
            
            if (orders.Count != 0)
            {
                SetDirection(orders[0]);
            }
            else
                direction = Vector2.zero;
        }

        transform.position += new Vector3(direction.x, direction.y, 0.0f) * mov_speed * Time.deltaTime;
	}

    public void ReceiveOrders(List<Signal.MoveDirection> data)
    {
        orders = data;
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
        looking_at = direction;
        orders.RemoveAt(0);
    }
}
