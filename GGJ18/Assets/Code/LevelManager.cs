using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

    public static LevelManager current_level;
    public float action_time = 2f;
    public Player player;

    public bool lost = false;
    public bool won = false;

	void Awake ()
    {
        if (current_level != null)
            Destroy(current_level);

        current_level = this;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update ()
    {
		
	}

    public void LossGame()
    {
        lost = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        won = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
