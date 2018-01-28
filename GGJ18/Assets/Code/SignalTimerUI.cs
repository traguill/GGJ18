using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalTimerUI : MonoBehaviour {

    public Signal signal;
    public Sprite[] numeros;
    public GameObject wifi_activations;
    Image image;
    bool ready = false;
    public GameObject start;
    private void Start()
    {
        image = GetComponent<Image>();
        wifi_activations.SetActive(false);
        start.SetActive(false);
        image.enabled = true;
    }

    void Update ()
    {
        ChangeActivations();
       float time = signal.GetLapsedTime();
       int i = Mathf.RoundToInt(time);
       if (i > numeros.Length - 1)
           i = numeros.Length - 1;

        if (i < 0)
            i = 0;

        image.sprite = numeros[i];

        if (i == 0)
            ready = true;
        else
            ready = false;
    }

    void ChangeActivations()
    {
        if(ready)
        {
            wifi_activations.SetActive(true);
            start.SetActive(true);
            image.enabled = false;
        }
        else
        {
            wifi_activations.SetActive(false);
            start.SetActive(false);
            image.enabled = true;
        }
    }
}
