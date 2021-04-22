using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTriggerScript : MonoBehaviour
{
    public float RateChange; //will be multiplied to the player's movement variables
    public bool isGoal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGoal)
        {
            if (other.GetComponent<PlayerScript>())
            {
                if (RateChange < 1.0f)
                {
                    other.GetComponent<PlayerScript>().jumpStrength *= RateChange;
                }
                other.GetComponent<PlayerScript>().moveSpeedRate *= RateChange;
                other.GetComponent<PlayerScript>().maxVelocityMag *= RateChange;
                other.GetComponent<Rigidbody>().velocity *= RateChange;
            }
        }
        else
        {
            if (other.GetComponent<PlayerScript>())
            {
                other.GetComponent<PlayerScript>().PlayerWins();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isGoal)
        {
            if (other.GetComponent<PlayerScript>())
            {
                if (RateChange < 1.0f)
                {
                    other.GetComponent<PlayerScript>().jumpStrength /= RateChange;
                }
                other.GetComponent<PlayerScript>().moveSpeedRate /= RateChange;
                other.GetComponent<PlayerScript>().maxVelocityMag /= RateChange;
            }
        }
    }
}
