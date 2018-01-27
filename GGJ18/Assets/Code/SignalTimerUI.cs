using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTimerUI : MonoBehaviour {

    public Signal signal;
    Vector3 direction;
	
	void Update ()
    {
        float angles = signal.GetLapsedTime() / signal.send_signal_every;

        angles *= 360;

        transform.rotation = Quaternion.Euler(0, 0, angles);
	}
}
