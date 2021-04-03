/*
@author $Seth Grimes$

@date $03/25/2021$
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController gc;

    [Tooltip("This is the sword that the player uses as their melee weapon.")]
    public GameObject sword;

    [Tooltip("This is how long the player must wait between sword swings.")]
    public float swordCooldown;

    [Tooltip("This is how long the player must wait for each mana to regenerate.")]
    public float regenTimer;

    // "gameOver" might be used in the future to check for if the game is over or not, "isSwinging" checks if the player is currently swinging their sword.
    // "isPaused" checks to see if the game is paused or not.
    private bool gameOver = false, isSwinging = false, isPaused = false;

    [Tooltip("This is how much mximum ammo the player has.")]
    public int ammoMax;

    // "ammo" is how much ammo the player has currently.
    private int ammo;

    [Tooltip("This is how much maximum mana the player has.")]
    public int manaMax;

    // "mana" is how much mana the player has currently.
    private int mana;

    [Tooltip("This is the text that displays the amount of ammo the player has left.")]
    public TextMeshProUGUI ammoText;

    // Awake() will just make sure that the sword isn't active when the game starts and then makes sure that this game object doesn't get destroyed when the
    // scene gets unloaded/loaded.
    void Awake()
    {
        ammo = ammoMax;
        mana = manaMax;
        sword.SetActive(false);
        DontDestroyOnLoad(gameObject);
        if (gc == null)
        {
            gc = this;
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            isPaused = !isPaused;
        
        if (!isPaused)
        {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        // This is currently just for testing to see if the variables remain the same between scenes.
        if (Input.GetKeyDown(KeyCode.M))
            if (SceneManager.GetActiveScene().name == "Game")
                SceneManager.LoadScene("Game2");
            else
                SceneManager.LoadScene("Game");

        // This makes sure that "mana" and "ammo" don't exceed their maximum values.
        if (mana > manaMax)
            mana = manaMax;
        if (ammo > ammoMax)
            ammo = ammoMax;

        // This updates the UI text for "ammo" and "mana".
        if (ammoText != null)
            ammoText.text = "Ammo : " + ammo + "\n" + "Mana : " + mana;
        
        // Don't forget to come back here and add Controller support once you figure it out
        if (Input.GetKeyDown(KeyCode.Q) && !isSwinging)
        {
            StartCoroutine("SwingSword");
        }

        RegenerateMana();
        }
    }

    // This just slowly regenerates the player's mana over time to the maximum amount of mana possible.
    public void RegenerateMana()
    {
        if (mana < manaMax)
        {
            regenTimer += Time.deltaTime;
            {
                if (regenTimer > 2.5)
                {
                    mana++;
                    regenTimer = 0;
                }
            }
        }
    }

    // SetAmmo() changes the "ammo" variable by the value passed to the parameter.
    public void SetAmmo(int value)
    {
        ammo += value;
    }
    
    // SetMana() changes the "mana" variable by the value passed to the parameter.
    public void SetMana(int value)
    {
        mana += value;
    }

    // getGameOver() returns the "gameOver" variable.
    public bool getGameOver()
    {
        return gameOver;
    }

    // GetMana() returns the "mana" variable.
    public int GetMana()
    {
        return mana;
    }

    // GetManaMax() returns the "manaMax" variable.
    public int GetManaMax()
    {
        return manaMax;
    }

    // GetAmmo() returns the "ammo" variable.
    public int GetAmmo()
    {
        return ammo;
    }

    // GetRegenTimer() returns the "regenTimer" variable.
    public float GetRegenTimer()
    {
        return regenTimer;
    }

    // This changes the variables associated with the player "swinging" their sword. It basically tells the game that the player is swining and needs to wait to swing
    // again.
    public IEnumerator SwingSword()
    {
        isSwinging = true;
        sword.SetActive(true);
        yield return new WaitForSeconds(swordCooldown);
        sword.SetActive(false);
        isSwinging = false;
    }

}