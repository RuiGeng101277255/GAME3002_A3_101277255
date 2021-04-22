using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessageScript : MonoBehaviour
{
    public float textDuration;
    public int MessageIndex; //0 is screen message, 1 is player livetext, 2 is keys collected
    public PlayerScript player;

    float tempDuration = 0.0f;
    TextMesh mess_TextMesh;
    bool winText = false;

    // Start is called before the first frame update
    void Start()
    {
        mess_TextMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (MessageIndex)
        {
            case 0: //Screen message
                //Check it's not an end result text (ie player won or lost)
                if (!winText)
                {
                    if (tempDuration > 0.0f)
                    {
                        tempDuration -= Time.deltaTime; //Display for only a period of time
                    }
                    else
                    {
                        mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    }
                }
                break;
            case 1: //Lives text
                mess_TextMesh.text = "Lives Remaining: " + player.PlayerInitialLiveCount.ToString();
                break;
            case 2: //Keys text
                mess_TextMesh.text = "Access to\nSecurity Lvl: " + player.LevelofSecurity.ToString();
                break;
        }
    }

    //Manipulates the transform since this is only a text mesh
    public void setTextDisplayed(string s, bool isWinText = false, bool didWin = false)
    {
        if (MessageIndex == 0)
        {
            mess_TextMesh.text = s;

            if (isWinText)
            {
                //If Player wins, text is green. Loses, text is red.
                if (didWin)
                {
                    mess_TextMesh.color = Color.green;
                }
                else
                {
                    mess_TextMesh.color = Color.red;
                }
                winText = isWinText;
            }
            else
            {
                mess_TextMesh.color = Color.red;
                tempDuration = textDuration;
            }
        }
    }
}
