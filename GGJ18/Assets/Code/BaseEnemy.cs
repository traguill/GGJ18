using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_ACTIONS
{
    MOVE,
    ROTATE_RIGHT,
    ROTATE_LEFT,
    LOOK_BACKWARDS,
    PAUSE
}

public class BaseEnemy : MonoBehaviour
{
    public ENEMY_ACTIONS[] my_actions;
    public float mov_velocity = 5f;
    public float max_movement_time = 0.5f;
    Vector2 grid_pos;
    int current_action = -1;
    Vector2 looking_at = Vector2.up;
    bool coming_back = false;

	void Start ()
    {
        grid_pos = Grid.RealWorldToGridPos(transform.position);
        SetPosInGrid(grid_pos);

        Invoke("DoAction", LevelManager.current_level.action_time);
    }
	
    void DoAction()
    {
        ENEMY_ACTIONS action_to_execute = ENEMY_ACTIONS.PAUSE;
        if(!coming_back)
        {
            current_action++;
            action_to_execute = my_actions[current_action];

            if (current_action == my_actions.Length - 1)
                current_action = 0;
        }
        else
        {
            switch (my_actions[current_action])
            {
                case ENEMY_ACTIONS.LOOK_BACKWARDS:
                    action_to_execute = ENEMY_ACTIONS.LOOK_BACKWARDS;
                    break;
                case ENEMY_ACTIONS.ROTATE_LEFT:
                    action_to_execute = ENEMY_ACTIONS.ROTATE_RIGHT;
                    break;
                case ENEMY_ACTIONS.ROTATE_RIGHT:
                    action_to_execute = ENEMY_ACTIONS.ROTATE_LEFT;
                    break;
                case ENEMY_ACTIONS.MOVE:
                    action_to_execute = ENEMY_ACTIONS.MOVE;
                    break;
                case ENEMY_ACTIONS.PAUSE:
                    action_to_execute = ENEMY_ACTIONS.PAUSE;
                    break;
            }
            
            if (current_action == 0)
                coming_back = false;

            current_action--;
        }

        ExecuteAction(action_to_execute);

        Invoke("DoAction", LevelManager.current_level.action_time);
    }

    void ExecuteAction(ENEMY_ACTIONS action)
    {
        //Debug.Log("EXECUTING ACTION:" + action.ToString());
        switch (action)
        {
            case ENEMY_ACTIONS.MOVE:
                Move(looking_at);
                break;
            case ENEMY_ACTIONS.ROTATE_RIGHT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, -90.0f) * looking_at;
                break;
            case ENEMY_ACTIONS.ROTATE_LEFT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, 90.0f) * looking_at;
                break;
            case ENEMY_ACTIONS.LOOK_BACKWARDS:
                looking_at = Quaternion.Euler(0.0f, 0.0f, 180.0f) * looking_at;
                break;
            case ENEMY_ACTIONS.PAUSE:

                break;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(looking_at.x, looking_at.y, 0.0f));
    }


    void Move(Vector3 dir)
    {
        if(isValidGridPos(grid_pos  + (Vector2)dir))
        {
            Vector3 new_pos = transform.position + dir * Grid.current_grid.real_units;
            StartCoroutine(MoveSmooth(new_pos));
            grid_pos += (Vector2)dir;
            SetGridPos();
        }

    }

    IEnumerator MoveSmooth(Vector3 pos)
    {
        float current_move_time = 0f;
        
        while(max_movement_time > current_move_time)
        {
            transform.position = Vector3.Lerp(transform.position, pos, mov_velocity * 0.02f);
            current_move_time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }

        transform.position = pos;
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

        if (Vector2.Distance(LevelManager.current_level.player.GetPosInGrid(), new_pos) < 0.1f)
            return false;

        return true;
    }

    public void SetPosInGrid(Vector2 pos)
    {
        grid_pos = pos;
        SetGridPos();
        Debug.Log(grid_pos);
    }
}
