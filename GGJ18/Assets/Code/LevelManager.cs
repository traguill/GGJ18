using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

    public static LevelManager current_level;
    public float action_time = 2f;
    public Player player;
    public Van van;
    public FadeInOut fade;

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
        van = GameObject.FindGameObjectWithTag("Van").GetComponent<Van>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeInOut>();
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
            van = GameObject.FindGameObjectWithTag("Van").GetComponent<Van>();
            fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeInOut>();
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
        StartCoroutine("WinFinalAnim");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator WinFinalAnim()
    {
        yield return new WaitForSeconds(2.0f);
        player.PlayJump();
        yield return new WaitForSeconds(1.3f);
        player.transform.position = van.player_spawn_position.position;
        player.PlayFall();
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(false);
        van.ExitVan();
        yield return new WaitForSeconds(3.0f);
        fade.FadeIn("end_game");
        yield return 0;
    }

    
}
