using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTriggerScript : MonoBehaviour
{
    public float RateChange; //will be multiplied to the player's movement variables

    float tempPlayerJumpRateInit;
    float tempPlayerMoveSpeedInit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(tempPlayerJumpRateInit);
        print(tempPlayerMoveSpeedInit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            tempPlayerJumpRateInit = other.GetComponent<PlayerScript>().jumpStrength * RateChange;
            tempPlayerMoveSpeedInit = other.GetComponent<PlayerScript>().moveSpeedRate * RateChange;
            other.GetComponent<PlayerScript>().jumpStrength *= RateChange;
            other.GetComponent<PlayerScript>().moveSpeedRate *= RateChange;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            other.GetComponent<PlayerScript>().jumpStrength = tempPlayerJumpRateInit;
            other.GetComponent<PlayerScript>().moveSpeedRate = tempPlayerMoveSpeedInit;
            tempPlayerJumpRateInit = 0.0f;
            tempPlayerMoveSpeedInit = 0.0f;
        }
    }
}
