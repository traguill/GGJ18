using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowInversedOrder : MonoBehaviour
{

    public Sprite UpArrow;
    public Sprite DownArrow;
    public Sprite LeftArrow;
    public Sprite WaitArrow;

    public GameObject arrow_img;

    List<Image> arrows;

    public Signal signal;
    public Player player;

    void Start()
    {
        arrows = new List<Image>();
        for (int i = 0; i < signal.max_orders; i++)
        {
            GameObject go = Instantiate(arrow_img, transform);
            arrows.Add(go.transform.GetChild(0).GetComponent<Image>());
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

        int j = signal.max_orders - 1;
        for (int i = 0; i < signal.max_orders; i++)
        {
            if (current_movements.Count > j - i)
            {
                arrows[i].transform.parent.gameObject.SetActive(true);
                switch (current_movements[j - i])
                {
                    case Signal.MoveDirection.UP:
                        arrows[i].sprite = UpArrow;
                        break;
                    case Signal.MoveDirection.RIGHT:
                        arrows[i].sprite = LeftArrow;
                        arrows[i].transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case Signal.MoveDirection.WAIT:
                        arrows[i].sprite = WaitArrow;
                        break;
                    case Signal.MoveDirection.LEFT:
                        arrows[i].sprite = LeftArrow;
                        break;
                }
            }
            else
            {
                if(arrows[i].gameObject.activeInHierarchy)
                {
                    arrows[i].transform.localScale = Vector3.one;

                    arrows[i].transform.parent.gameObject.SetActive(false);
                }
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
