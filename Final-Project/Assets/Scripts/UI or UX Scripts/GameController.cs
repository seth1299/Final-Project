using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

<<<<<<< Updated upstream
    // Commenting this out for now since we might get an animation for this instead.
    //[Tooltip("This is the starting position of the sword. Just put in the Sword Starting Position game object that's attached to the Player.")]
    //public Transform swordStartPosition;
=======
    [Tooltip("This is the same game object that this script is put onto.")]
    public static GameController gc;
>>>>>>> Stashed changes

    [Tooltip("This is the sword that the player uses as their melee weapon.")]
    public GameObject sword;

    [Tooltip("This is how long the player must wait between sword swings.")]
    public float swordCooldown;

<<<<<<< Updated upstream
    private bool gameOver = false, isSwinging = false;
=======
    [Tooltip("This is the bow that the player uses as their ranged weapon.")]
    public GameObject bow;

    [Tooltip("This is how long the player must wait for each mana to regenerate. Negative values make it longer, while positive values make it shorter.")]
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
>>>>>>> Stashed changes

    void Start()
    {
        sword.SetActive(false);
<<<<<<< Updated upstream
=======
        bow.SetActive(false);
        DontDestroyOnLoad(gameObject);
        if (gc == null)
        {
            gc = this;
        }
        else if (gc != this)
        {
            gc.UpdateThisScript(this);
            Destroy(gameObject);
        }
>>>>>>> Stashed changes
    }

    public void UpdateThisScript(GameController GC_Script)
    {
        sword = GC_Script.sword;
        bow = GC_Script.bow;
        ammoText = GC_Script.ammoText;
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
=======
        // This checks if the player is pressing the button to pause the game.
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
            isPaused = !isPaused;
        
        // This makes sure that the game controller isn't updating game variables while the game is paused.
        if (!isPaused)
        {

        if (Input.GetButtonDown("Quit Keyboard") || Input.GetButtonDown("Quit Controller"))
            Application.Quit();
        
        // This is currently just for testing to see if the variables remain the same between scenes. DELETE THIS ONCE ACTUAL LEVELS ARE IMPLEMENTED
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
        
>>>>>>> Stashed changes
        // Don't forget to come back here and add Controller support once you figure it out
        if (Input.GetButtonDown("Sword Attack Keyboard") || Input.GetButtonDown("Sword Attack Controller") && !isSwinging)
        {
            //Destroy(Instantiate(sword, swordStartPosition, swordStartPosition), 2f);
            StartCoroutine("SwingSword");
        }
<<<<<<< Updated upstream
=======

        // This is for shooting the player's bow.
        if (Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") && !isSwinging)
        {
            ShootBow();
        }

        // This is for doing the cure ability.
        if (Input.GetButtonDown("Cure Ability Keyboard") || Input.GetButtonDown("Cure Ability Controller") && !isSwinging)
        {
            DoMagic();
        }

        // This is for unequiping everything.
        if (Input.GetButtonDown("Unequip Everything Keyboard") || Input.GetButtonDown("Unequip Everything Controller") && !isSwinging)
        {
            DoMagic();
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
>>>>>>> Stashed changes
    }

    public bool getGameOver()
    {
        return gameOver;
    }

<<<<<<< Updated upstream
=======
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

    // This changes the variables associated with the player "swinging" their sword. It basically tells the game that the player is swinging and needs to wait to swing
    // again.
>>>>>>> Stashed changes
    public IEnumerator SwingSword()
    {
        isSwinging = true;
        if (!sword.activeSelf)
            sword.SetActive(true);
        if (bow.activeSelf)
            bow.SetActive(false);
        yield return new WaitForSeconds(swordCooldown);
        isSwinging = false;
    }

    // This changes the variables associated with the player "shooting" their bow. It basically tells the game that the player is shooting their bow and needs to wait 
    // to shoot again.
    public void ShootBow()
    {
        if (sword.activeSelf)
            sword.SetActive(false);
        if (!bow.activeSelf)
            bow.SetActive(true);
    }
    public void DoMagic()
    {
        if (sword.activeSelf)
            sword.SetActive(false);
        if (bow.activeSelf)
            bow.SetActive(false);
    }

}