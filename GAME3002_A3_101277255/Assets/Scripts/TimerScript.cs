using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float maxtime = 120.0f; //120 secs in 2 min

    TextMesh timer_txt;

    // Start is called before the first frame update
    void Start()
    {
        timer_txt = GetComponent<TextMesh>();
        getModulusMode();
    }

    // Update is called once per frame
    void Update()
    {
        maxtime -= Time.deltaTime;
        getModulusMode();
    }

    private void getModulusMode() // Modulus arithmetic for separating min, sec and millisec from timer of unit seconds
    {
        float sec = maxtime % 60.0f;
        float millisec = Mathf.Abs((sec - Mathf.Round(sec)) * 100.0f); //For later to display 2 digits of the milliseconds
        float min = (maxtime - sec) / 60.0f;
        timer_txt.text = Mathf.Round(min) + ": " + Mathf.Round(sec) + ": " + Mathf.Round(millisec);
    }
}
