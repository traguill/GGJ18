using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour 
{
    public enum MoveDirection
    {
        UP,
        RIGHT,
        WAIT,
        LEFT
    }

    float timer = 0.0f;
    public float send_signal_every = 5.0f; //Sends signal every x seconds

    public Player player;
	
    //Save player input
    List<MoveDirection> orders_buffer = new List<MoveDirection>();

    public int max_orders = 10;//Functionality not done, just for UI. 


	void Update () 
    {
        //Save player input
        GetPlayerInput();    

        timer += Time.deltaTime;
        if(timer >= send_signal_every)
        {
            timer -= send_signal_every;
            SendSignal();
        }
	}

    void SendSignal()
    {
        player.ReceiveOrders(orders_buffer);
        orders_buffer.Clear();
    }

    private void GetPlayerInput()
    {
        if (Input.GetKeyUp(KeyCode.Space)) orders_buffer.Add(MoveDirection.UP);
        if (Input.GetKeyUp(KeyCode.J)) orders_buffer.Add(MoveDirection.RIGHT);
        if (Input.GetKeyUp(KeyCode.Y)) orders_buffer.Add(MoveDirection.WAIT);
        if (Input.GetKeyUp(KeyCode.F)) orders_buffer.Add(MoveDirection.LEFT);

        if (Input.GetKeyUp(KeyCode.Backspace) && orders_buffer.Count > 0) orders_buffer.RemoveAt(orders_buffer.Count - 1);
        if (Input.GetKeyUp(KeyCode.Delete) && orders_buffer.Count != 0) orders_buffer.Clear();
    }

    public List<MoveDirection> GetOrders()
    {
        return orders_buffer;
    }

    public float GetTimeUntilSignal()
    {
        return send_signal_every - timer;
    }

    public float GetLapsedTime()
    {
        return send_signal_every - timer;
    }
}
