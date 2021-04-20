using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float maxtime = 120.0f; //120 secs in 2 min

    TextMesh timer_txt;
    float sec;
    float millisec;
    float min;

    // Start is called before the first frame update
    void Start()
    {
        timer_txt = GetComponent<TextMesh>();
        min = 2;
        sec = 0;
        millisec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        getModulusMode();
        maxtime -= Time.deltaTime;
    }

    private void getModulusMode() // Modulus arithmetic for separating min, sec and millisec from timer of unit seconds
    {
        //To allow 00 to show when the value is 0, also to put a 0 infront of single digits
        string secText = sec.ToString();
        string millisecText = millisec.ToString();
        string minText = min.ToString();

        if ((sec < 10.0f) && (sec > 0.0f))
        {
            secText = "0" + sec.ToString();
        }
        else if (sec <= 0.0f)
        {
            secText = "00";
        }

        if ((millisec < 10.0f) && (millisec > 0.0f))
        {
            millisecText = "0" + millisec.ToString();
        }
        else if (millisec <= 0.0f)
        {
            millisecText = "00";
        }

        if ((min < 10.0f) && (min > 0.0f))
        {
            minText = "0" + min.ToString();
        }
        else if (sec <= min)
        {
            minText = "00";
        }

        timer_txt.text = minText + ": " + secText + ": " + millisecText;

        //Updating timer part
        sec = maxtime % 60;
        millisec = Mathf.Abs((sec - Mathf.Round(sec)) * 100.0f); //For 2 digits of the milliseconds
        min = (maxtime - sec) / 60.0f;

        //setting 2 digits rounds
        sec = Mathf.Round(sec);
        millisec = Mathf.Round(millisec);
        min = Mathf.Round(min);
    }
}
