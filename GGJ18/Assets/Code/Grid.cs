using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int n_w = 12;
    public int n_h = 12;
    public int real_units = 26;
    public static int w = 12;
    public static int h = 12;
    public static int x = 0;
    public static int y = 0;
    public static Transform[,] grid = new Transform[w, h];

    public GameObject grid_element;

    public static Grid current_grid;

    // Use this for initialization
    void Awake()
    {
        if (current_grid != null)
            Destroy(current_grid);

        current_grid = this;

        w = n_w;
        h = n_h;

        grid = new Transform[w, h];

        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Temporary just for showing the grid
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                GameObject go = Instantiate(grid_element, transform.position + new Vector3(i * real_units, (n_h - 1) * real_units - j * real_units), Quaternion.identity);
            }
        }
   }

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2((int)(v.x),(int)(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= x &&
                (int)pos.x < x + w &&
                (int)pos.y < y + h &&
                (int)pos.y >= y);
    }

    public static void AddTransformToGrid(Vector2 pos, Transform t)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (grid[i, j] == t)
                {
                    grid[i, j] = null;
                }
            }
        }

        grid[(int)pos.x - x, (int)pos.y - y] = t;
    }

    public static bool PositionFree(Vector2 pos)
    {
        if (grid[(int)pos.x - x, (int)pos.y - y] == null)
        {
            return true;
        }

        return false;
    }

    public static int YpositionInGrid(Vector2 pos)
    {
        return (int)pos.y - y;
    }

    public static Transform TransformInPos(Vector2 pos)
    {
        return grid[(int)pos.x - x, (int)pos.y - y];
    }

    public static Vector2 RealWorldToGridPos(Vector3 pos)
    {
        Vector2 n_pos = pos - current_grid.transform.position;
        n_pos /= current_grid.real_units;

        n_pos.x = Mathf.FloorToInt(n_pos.x);
        n_pos.y = Mathf.FloorToInt(n_pos.y);
        return n_pos;
    }

    //Helpers ------------------------------------------------------------------
    static Transform GetChildByTag(Transform parent, string tag)
    {
        foreach (Transform t in parent)
        {
            if (t.tag == tag)
                return t;
        }

        return null;
    }
}

