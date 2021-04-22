using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessageScript : MonoBehaviour
{
    public float textDuration;
    public bool isPlayerLiveText;
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
        if (!isPlayerLiveText)
        {
            if (tempDuration > 0.0f)
            {
                tempDuration -= Time.deltaTime;
            }
            else
            {
                mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
        else
        {
            mess_TextMesh.text = "Lives: " + player.PlayerInitialLiveCount.ToString();
        }
    }

    //Manipulates the transform since this is only a text mesh
    public void setTextDisplayed(string s)
    {
        if (!isPlayerLiveText)
        {
            mess_TextMesh.text = s;
            mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            tempDuration = textDuration;
        }
    }
}
