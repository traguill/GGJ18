using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    int state;
    public OptionsMenu options;
    public QuestionMenu question;
    public AudioSource audio_source;
    public AudioClip select_level;

    public static MainMenu menu;

    public MonkeyPortrait monkey;

    void Awake()
    {
        if(MainMenu.menu != null)
        {
            Destroy(gameObject);
            return;
        }
            menu = this;
    }

    void Start()
    {
        if (LevelManager.current_level == null)
            state = 0;
        else
            if (LevelManager.current_level.won)
                state = 2;
            else
                state = 3;
        question.AskQuestion(state);
    }

    public void EndAsking()
    {
        options.ShowOptions(state);
    }

    public void OptionSelected(int id)
    {
        switch (state)
        {
            case 0: //menu
                MenuOptions(id);
                break;
            case 1: //level selector
                LevelSelectorOptions(id);
                break;
            case 2: //win
                EndLvlOptions(id);
                break;
            case 3: //lose
                EndLvlOptions(id);
                break;
        }
    }

    void MenuOptions(int id)
    {
        if(id == 1)
        {
            state = 1;
            question.AskQuestion(state);
        }

        if(id == 2)
        {
            Debug.Log("Credits");
        }

        if (id == 3)
            Application.Quit();
    }

    void LevelSelectorOptions(int id)
    {
        if (id == 1) //play lvl 1
        {
            audio_source.PlayOneShot(select_level);
            StartCoroutine(ChangeScene("IntroScene1"));
        }

        if (id == 2) //play lvl 2
        {
            audio_source.PlayOneShot(select_level);
            StartCoroutine(ChangeScene("IntroScene2"));
        }

        if (id == 3) //return to menu
        {
            state = 0;
            question.AskQuestion(state);
        }
    }

    void EndLvlOptions(int id)
    {
        if(id == 1)
        {
            if (LevelManager.current_level)
            {
                if (LevelManager.current_level.scene_index != -1)
                    SceneManager.LoadScene(LevelManager.current_level.scene_index);
            }
        }

        if(id == 2)
        {
            state = 0;
            question.AskQuestion(state);
        }
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
}
