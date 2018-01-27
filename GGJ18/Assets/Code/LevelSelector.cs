using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour 
{

    public int num_levels = 5;
    public int max_level = 1;

	// Use this for initialization
	void Start () 
    {
        UpdateUnlockedLevels();
	}
	
	// Update is called once per frame
	void Update () 
    {
	   if(Input.GetKeyUp(KeyCode.UpArrow))
       {
           max_level += 1;
           if (max_level > num_levels)
               max_level = num_levels;
           PlayerPrefs.SetInt("level", max_level);
           PlayerPrefs.Save();
       }
       if(Input.GetKeyUp(KeyCode.DownArrow))
       {
           max_level -= 1;
           if (max_level < 1)
               max_level = 1;
           PlayerPrefs.SetInt("level", max_level);
           PlayerPrefs.Save();
       }
	}

    void UpdateUnlockedLevels()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            max_level = PlayerPrefs.GetInt("level");
        }
        else
        {
            max_level = 1;
        }
    }
}
