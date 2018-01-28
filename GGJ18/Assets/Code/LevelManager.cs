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
    public int scene_index = -1;

	void Awake ()
    {
        if (current_level != null)
        {
            Destroy(gameObject);
            return;
         }
        current_level = this;
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");

        if (tmp)
        {
            player = tmp.GetComponent<Player>();
            lost = false;
            won = false;
        }
    }

    public void LossGame()
    {
        lost = true;
        scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("end_game");
    }

    public void WinGame()
    {
        won = true;
        scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("end_game");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
}
