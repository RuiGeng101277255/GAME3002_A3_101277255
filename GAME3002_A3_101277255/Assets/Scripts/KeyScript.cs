using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>())
        {
            collision.gameObject.GetComponent<PlayerScript>().LevelofSecurity += 1;
            Destroy(gameObject);
        }
    }
}
