using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWalkable : MonoBehaviour {

    Vector2 grid_pos;
	void Start ()
    {
        grid_pos = Grid.RealWorldToGridPos(transform.position);
        SetPosInGrid(grid_pos);
    }

    public void SetGridPos()
    {
        if (isValidGridPos(grid_pos))
        {
            Vector2 v = Grid.roundVec2(grid_pos);
            Grid.AddTransformToGrid(v, transform);
        }
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

    public void SetPosInGrid(Vector2 pos)
    {
        grid_pos = pos;
        SetGridPos();
    }
}
