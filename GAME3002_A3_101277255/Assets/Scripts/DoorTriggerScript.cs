using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public int KeysRequired;
    public Light lightObj;
    public ScreenMessageScript screenMessage;

    HingeJoint door_Hinge;

    // Start is called before the first frame update
    void Start()
    {
        door_Hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!door_Hinge.useMotor)
        {
            if (other.GetComponent<PlayerScript>())
            {
                if (other.GetComponent<PlayerScript>().LevelofSecurity >= KeysRequired)
                {
                    lightObj.color = Color.green;
                    door_Hinge.useMotor = true;
                }
                else
                {
                    lightObj.color = Color.red;
                    screenMessage.setTextDisplayed("NOT ENOUGH KEYS\nTO OPEN DOOR");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!door_Hinge.useMotor)
        {
            if (other.GetComponent<PlayerScript>())
            {
                lightObj.color = Color.white;
            }
        }
    }
}
