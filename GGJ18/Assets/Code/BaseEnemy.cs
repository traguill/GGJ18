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
    public Vector2 looking_at = Vector2.up;
    bool coming_back = false;


    SpriteRenderer s_ren;
    Animator anim;
	void Start ()
    {
        grid_pos = Grid.RealWorldToGridPos(transform.position);
        SetPosInGrid(grid_pos);

        Invoke("DoAction", LevelManager.current_level.action_time);
        s_ren = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (looking_at.x > 0)
            s_ren.flipX = true;
    }
	
    void DoAction()
    {
        ENEMY_ACTIONS action_to_execute = ENEMY_ACTIONS.PAUSE;
        if(!coming_back)
        {
            current_action++;
            action_to_execute = my_actions[current_action];

            if (current_action == my_actions.Length - 1)
                current_action = -1;
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
        s_ren.flipX = false;
        Vector2 old_looking = looking_at;
        switch (action)
        {
            case ENEMY_ACTIONS.MOVE:
                Move(looking_at);
                break;
            case ENEMY_ACTIONS.ROTATE_RIGHT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, -90.0f) * looking_at;
                if (Mathf.RoundToInt(Mathf.Abs(looking_at.x)) > 0.1f)
                    anim.SetTrigger("normalidle");

                if (Mathf.RoundToInt(looking_at.y) > 0.2f)
                    anim.SetTrigger("goup");

                if (Mathf.RoundToInt(looking_at.y) < -0.2f)
                    anim.SetTrigger("godown");
                break;
            case ENEMY_ACTIONS.ROTATE_LEFT:
                looking_at = Quaternion.Euler(0.0f, 0.0f, 90.0f) * looking_at;
                if (Mathf.RoundToInt(Mathf.Abs(looking_at.x)) > 0.1f)
                    anim.SetTrigger("normalidle");

                if (Mathf.RoundToInt(looking_at.y) > 0.2f)
                    anim.SetTrigger("goup");

                if (Mathf.RoundToInt(looking_at.y) < -0.2f)
                    anim.SetTrigger("godown");

                break;
            case ENEMY_ACTIONS.LOOK_BACKWARDS:
                looking_at = Quaternion.Euler(0.0f, 0.0f, 180.0f) * looking_at;
                if (Mathf.RoundToInt(looking_at.y) > 0.2f)
                    anim.SetTrigger("goup");

                if (Mathf.RoundToInt(looking_at.y) < -0.2f)
                    anim.SetTrigger("godown");
                break;
            case ENEMY_ACTIONS.PAUSE:

                break;
        }
        
        if (looking_at.x > 0)
            s_ren.flipX = true;
        else
            s_ren.flipX = false;

        Detect();
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(looking_at.x, looking_at.y, 0.0f));
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
        anim.SetFloat("speedy", pos.y - transform.position.y);
        anim.SetFloat("speedx", Mathf.Abs(pos.x - transform.position.x));
        while (max_movement_time > current_move_time)
        {
            transform.position = Vector3.Lerp(transform.position, pos, mov_velocity * Time.deltaTime);
            current_move_time += 0.02f;
            anim.SetFloat("speedy", Mathf.RoundToInt(pos.y - transform.position.y));
            anim.SetFloat("speedx", Mathf.Abs(pos.x - transform.position.x));
            s_ren.sortingOrder = (int)grid_pos.y;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        transform.position = pos;
    }

    void Detect()
    {

        for(int i = 0; i< 3; i++)
        {
            RaycastHit2D hit;
            if (i== 0)
            {
                hit = Physics2D.Raycast(transform.position, looking_at, 1000);
            }
            else if(i == 1)
            {
                hit = Physics2D.Raycast(transform.position + new Vector3(-looking_at.y, looking_at.x) * Grid.current_grid.real_units + new Vector3(looking_at.x, looking_at.y) * Grid.current_grid.real_units, looking_at, 1000);

            }
            else
            {
                hit = Physics2D.Raycast(transform.position - new Vector3(-looking_at.y, looking_at.x) * Grid.current_grid.real_units + new Vector3(looking_at.x, looking_at.y) * Grid.current_grid.real_units, looking_at, 1000);
            }
                
            if (hit.collider != null)
            {
                if(hit.collider.CompareTag("Player"))
                {
                    LevelManager.current_level.LossGame();
                }
            }
        }
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
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                Debug.DrawRay(transform.position, looking_at * 1000, Color.yellow);
            }
            else if (i == 1)
            {
                Debug.DrawRay((transform.position + new Vector3(-looking_at.y, looking_at.x) * Grid.current_grid.real_units)+ new Vector3(looking_at.x, looking_at.y) * Grid.current_grid.real_units, looking_at * 1000, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position - (new Vector3(-looking_at.y, looking_at.x) * Grid.current_grid.real_units) + new Vector3(looking_at.x, looking_at.y) * Grid.current_grid.real_units, looking_at * 1000, Color.yellow);
            }
        }
    }
}
