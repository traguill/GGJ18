using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowInversedOrder : MonoBehaviour
{

    public Sprite UpArrow;
    public Sprite DownArrow;
    public Sprite LeftArrow;
    public Sprite RightArrow;

    public GameObject arrow_img;

    List<Animator> arrows;

    public Signal signal;
    public Player player;

    void Start()
    {
        arrows = new List<Animator>();
        for (int i = 0; i < signal.max_orders; i++)
        {
            GameObject go = Instantiate(arrow_img, transform);
            arrows.Add(go.GetComponent<Animator>());
        }
    }

    void Update()
    {
        List<Signal.MoveDirection> current_movements;

        if (player != null)
            current_movements = player.GetOrders();
        else
            current_movements = signal.GetOrders();

        int j = signal.max_orders - 1;
        for (int i = 0; i < signal.max_orders; i++)
        {
            if (current_movements.Count > j - i)
            {
                arrows[i].gameObject.SetActive(true);
                switch (current_movements[j - i])
                {
                    case Signal.MoveDirection.UP:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("up", true);
                        break;
                    case Signal.MoveDirection.RIGHT:
                        DeactivateOthers(arrows[i]);
                        arrows[i].SetBool("left", true);
                        arrows[i].transform.localScale = new Vector3(-1, 1, 1);
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
                arrows[i].transform.localScale = Vector3.one;
                arrows[i].SetBool("up", false);
                arrows[i].SetBool("wait", false);
                arrows[i].SetBool("left", false);
                arrows[i].gameObject.SetActive(false);
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
