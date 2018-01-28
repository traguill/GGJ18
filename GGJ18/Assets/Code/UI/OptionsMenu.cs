using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestionOptions
{
    public string name;
    public int id_question;
    public int max_options;
    public string option1;
    public string option2;
    public string option3;
}

public class OptionsMenu : MonoBehaviour {

    public Text option1;
    public Text option2;
    public Text option3;
    public Image arrow;

    public AudioClip cursor;
    public AudioSource audio_source;

    public QuestionOptions[] sets;

    int max_options = 3;
    int selected_option = 1;

    bool is_visible = false;

    QuestionOptions qo_selected = null;

    void Start()
    {
        option1.enabled = is_visible;
        option2.enabled = is_visible;
        option3.enabled = is_visible;
        arrow.enabled = is_visible;
    }

    void Reset()
    {
        is_visible = false;
        selected_option = 1;
        option1.enabled = is_visible;
        option2.enabled = is_visible;
        option3.enabled = is_visible;
        arrow.enabled = is_visible;
    }

    void Update()
    {
        if(is_visible)
        {
            //Navigable
            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                SetOptionDown();
                audio_source.PlayOneShot(cursor);
            }
            if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                SetOptionUp();
                audio_source.PlayOneShot(cursor);
            }

            if(Input.GetKeyUp(KeyCode.Return))
            {
                MainMenu.menu.OptionSelected(selected_option);
                audio_source.PlayOneShot(cursor);
                Reset();
            }
        }
    }

    public void ShowOptions(int set)
    {
        qo_selected = sets[set];
        is_visible = true;

        option1.enabled = is_visible;
        if(qo_selected.max_options > 1)
            option2.enabled = is_visible;
        if(qo_selected.max_options > 2)
            option3.enabled = is_visible;

        selected_option = 1;
        option1.fontStyle = FontStyle.Italic;
        option1.fontSize = option1.fontSize + 4;

        option1.text = qo_selected.option1;
        option2.text = qo_selected.option2;
        option3.text = qo_selected.option3;

        arrow.enabled = is_visible;
        arrow.rectTransform.localPosition = new Vector3(arrow.rectTransform.localPosition.x, -226, arrow.rectTransform.localPosition.z);
    }

    void SetOptionDown()
    {
       if(selected_option < 3 && selected_option < qo_selected.max_options)
       {
           selected_option++;
           switch (selected_option)
           {
               case 2:
                   option1.fontStyle = FontStyle.Normal;
                   option2.fontStyle = FontStyle.Italic;
                   option1.fontSize = option1.fontSize - 4;
                   option2.fontSize = option2.fontSize + 4;
                   arrow.rectTransform.localPosition += new Vector3(0.0f, -62.0f, 0.0f);
                   break;
               case 3:
                   option2.fontStyle = FontStyle.Normal;
                   option3.fontStyle = FontStyle.Italic;
                   option2.fontSize = option2.fontSize - 4;
                   option3.fontSize = option3.fontSize + 4;
                   arrow.rectTransform.localPosition += new Vector3(0.0f, -62.0f, 0.0f);
                   break;
           }
       }
    }

    void SetOptionUp()
    {
        if (selected_option != 1)
        {
            selected_option--;
            switch (selected_option)
            {
                case 1:
                    option2.fontStyle = FontStyle.Normal;
                    option1.fontStyle = FontStyle.Italic;
                    option2.fontSize = option2.fontSize - 4;
                    option1.fontSize = option1.fontSize + 4;
                    arrow.rectTransform.localPosition += new Vector3(0.0f, 62.0f, 0.0f);
                    break;
                case 2:
                    option3.fontStyle = FontStyle.Normal;
                    option2.fontStyle = FontStyle.Italic;
                    option3.fontSize = option3.fontSize - 4;
                    option2.fontSize = option2.fontSize + 4;
                    arrow.rectTransform.localPosition += new Vector3(0.0f, 62.0f, 0.0f);
                    break;
            }
        }
    }
}
