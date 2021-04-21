using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessageScript : MonoBehaviour
{
    public float textDuration;

    float tempDuration = 0.0f;
    TextMesh mess_TextMesh;

    // Start is called before the first frame update
    void Start()
    {
        mess_TextMesh = GetComponent<TextMesh>();
        mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
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

    //Manipulates the transform since this is only a text mesh
    public void setTextDisplayed(string s)
    {
        mess_TextMesh.text = s;
        mess_TextMesh.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        tempDuration = textDuration;
    }
}
