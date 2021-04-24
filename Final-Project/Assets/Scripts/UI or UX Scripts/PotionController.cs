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
        if (enteree.gameObject.tag == "Player" && GameObject.FindWithTag("GameController").GetComponent<GameController>().GetHealth() < GameObject.FindWithTag("GameController").GetComponent<GameController>().GetHealthMax() && this.gameObject.CompareTag("Health"))
        {
            Debug.Log("Setting health");
            enteree.GetComponent<PlayerController>().OpenHealthMod(10);
            Destroy(gameObject);
        }
        else if (enteree.gameObject.tag == "Player" && GameObject.FindWithTag("GameController").GetComponent<GameController>().GetMana() < GameObject.FindWithTag("GameController").GetComponent<GameController>().GetManaMax() && this.gameObject.CompareTag("Mana"))
        {
            Debug.Log("Setting mana");
            enteree.GetComponent<PlayerController>().SetMana(10);
            Destroy(gameObject);
        }
    }
}
