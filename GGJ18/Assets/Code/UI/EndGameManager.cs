using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void ReloadLastScene()
    {
        if(LevelManager.current_level)
        {
            if (LevelManager.current_level.scene_index != -1)
                SceneManager.LoadScene(LevelManager.current_level.scene_index);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
