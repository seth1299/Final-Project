using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Stuff, I don't know, aaaa.
    }
    void OnTriggerEnter(Collider enteree)
    {
        if (enteree.gameObject.tag == "Player" && GameObject.FindWithTag("GameController").GetComponent<GameController>().GetHealth() < GameObject.FindWithTag("GameController").GetComponent<GameController>().GetHealthMax())
        {
            enteree.GetComponent<PlayerController>().OpenHealthMod(10);
            Destroy(gameObject);
        }
    }
}
