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
            case 0: //Screen
                if (tempDuration > 0.0f)
                {
                    tempDuration -= Time.deltaTime;
                }
                else
                {
                    mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
                break;
            case 1: //Lives
                mess_TextMesh.text = "Lives: " + player.PlayerInitialLiveCount.ToString();
                break;
            case 2: //Keys
                mess_TextMesh.text = "Access to\nSecurity Lvl: " + player.LevelofSecurity.ToString();
                break;
        }
    }

    //Manipulates the transform since this is only a text mesh
    public void setTextDisplayed(string s)
    {
        if (MessageIndex == 0)
        {
            mess_TextMesh.text = s;
            mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            tempDuration = textDuration;
        }
    }
}
