using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // Commenting this out for now since we might get an animation for this instead.
    //[Tooltip("This is the starting position of the sword. Just put in the Sword Starting Position game object that's attached to the Player.")]
    //public Transform swordStartPosition;

    [Tooltip("This is the sword that the player uses as their melee weapon.")]
    public GameObject sword;

    [Tooltip("This is how long the player must wait between sword swings.")]
    public float swordCooldown;

    private bool gameOver = false, isSwinging = false;

    void Start()
    {
        sword.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        // Don't forget to come back here and add Controller support once you figure it out
        if (Input.GetKeyDown(KeyCode.Q) && !isSwinging)
        {
            //Destroy(Instantiate(sword, swordStartPosition, swordStartPosition), 2f);
            StartCoroutine("SwingSword");
        }
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public IEnumerator SwingSword()
    {
        isSwinging = true;
        sword.SetActive(true);
        yield return new WaitForSeconds(swordCooldown);
        sword.SetActive(false);
        isSwinging = false;
    }

}