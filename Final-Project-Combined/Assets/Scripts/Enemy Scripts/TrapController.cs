using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // public GameObject healingBottle;

    void Update()
    {
        // I don't know what should really go here.
    }
    void OnTriggerEnter(Collider enteree)
    {
        if (enteree.gameObject.tag == "Player")
        {
            if ( enteree.GetComponent<PlayerController>().powerUp == true)
            {
                // Instantiate(healingBottle, gameObject.transform);
                enteree.GetComponent<PlayerController>().OpenHealthMod(10);
                Destroy(gameObject);
            }
            else
            {
                enteree.GetComponent<PlayerController>().Snare();
                Destroy(gameObject);
            }
        }
    }
}
