using System.Collections;
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

    List<Image> arrows;

    public Signal signal;
    public Player player;

    void Start()
    {
        arrows = new List<Image>();
        for(int i = 0; i < signal.max_orders; i++)
        {
            GameObject go = Instantiate(arrow_img, transform);
            arrows.Add(go.GetComponent<Image>());
            go.SetActive(false);
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
                switch (current_movements[i])
                {
                    case Signal.MoveDirection.UP:
                        arrows[i].sprite = UpArrow;
                        break;
                    case Signal.MoveDirection.RIGHT:
                        arrows[i].sprite = RightArrow;
                        break;
                    case Signal.MoveDirection.WAIT:
                        arrows[i].sprite = DownArrow;
                        break;
                    case Signal.MoveDirection.LEFT:
                        arrows[i].sprite = LeftArrow;
                        break;
                }
                arrows[i].gameObject.SetActive(true);
            }
            else
                arrows[i].gameObject.SetActive(false);
        }
    }


}