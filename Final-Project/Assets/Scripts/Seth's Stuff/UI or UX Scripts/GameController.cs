using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    [Tooltip("This is the same game object that this script is put onto.")]
    public static GameController gc;

    [Tooltip("This is the sword that the player uses as their melee weapon.")]
    public GameObject sword;

    [Tooltip("This is how long the player must wait between sword swings.")]
    public float swordCooldown;

    [Tooltip("This is the bow that the player uses as their ranged weapon.")]
    public GameObject bow;

    [Tooltip("This is how long the player must wait for each mana to regenerate. Negative values make it longer, while positive values make it shorter.")]
    public float regenTimer;

    // "gameOver" might be used in the future to check for if the game is over or not, "isSwinging" checks if the player is currently swinging their sword.
    // "isPaused" checks to see if the game is paused or not.
    private bool gameOver = false, isSwinging = false, isPaused = false, playerCuredAllEnemies = false, playerIsDead = false, justStarted = true;

    [Tooltip("This is how much mximum ammo the player has.")]
    public int ammoMax;

    [Tooltip("This is the maximum amount of health that the player can have.")]
    public int healthMax;

    // "ammo" is how much ammo the player has currently, "mana" is how much mana the player has currently, and "health" is how much health the player has currently.
    private int ammo, mana, health = 1;

    [Tooltip("This is how much maximum mana the player has.")]
    public int manaMax;

    //[Tooltip("This is the text where the player's ammo and mana is displayed on. Will be changed into the UI bar eventually.")]
    public TextMeshProUGUI ammoText;

    [Tooltip("This is the animator component of the player's Sword.")]
    public Animator anim;

    public AudioSource bow_SFX;

    void Awake()
    {
        StartCoroutine("CheckForGameOver");
        Cursor.lockState = CursorLockMode.None;
    }

    public IEnumerator CheckForGameOver()
    {
        yield return new WaitForSeconds(2.0f);

        while ( ( !playerIsDead && !playerCuredAllEnemies ) )
        {
            CheckIfPlayerHasCuredAllSlimes();

            CheckIfPlayerDead();

            CheckIfGameOver();

            yield return null;
        }
    }

    void Start()
    {
        ammo = ammoMax;
        mana = manaMax;
        health = healthMax;
        sword.SetActive(false);
        bow.SetActive(false);
        if ( SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "Defeat" )
        {
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateThisScript(GameController GC_Script)
    {
        ammo = GC_Script.ammo;
        ammoMax = GC_Script.ammoMax;
        ammoText = GC_Script.ammoText;
        anim = GC_Script.anim;
        bow = GC_Script.bow;
        swordCooldown = GC_Script.swordCooldown;        
        bow_SFX = GC_Script.bow_SFX;
        gameOver = GC_Script.gameOver;
        health = GC_Script.health;
        isSwinging = GC_Script.isSwinging;
        isPaused = GC_Script.isPaused;
        justStarted = GC_Script.justStarted;
        manaMax = GC_Script.manaMax;
        mana = GC_Script.mana;
        playerCuredAllEnemies = GC_Script.playerCuredAllEnemies;
        playerIsDead = GC_Script.playerIsDead;
        regenTimer = GC_Script.regenTimer;
        sword = GC_Script.sword;
    }

    void Update()
    {
        if ( ( !playerIsDead && !playerCuredAllEnemies ) && ( SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "Defeat") )
        {
        /*
        if (!justStarted)
        {
        CheckIfPlayerHasCuredAllSlimes();

        CheckIfPlayerDead();

        CheckIfGameOver();
        }
        */
        // This checks if the player is pressing the button to pause the game.
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
            isPaused = !isPaused;
        
        // This makes sure that the game controller isn't updating game variables while the game is paused.
        if (!isPaused)
        {

        if (Input.GetButtonDown("Quit Keyboard") || Input.GetButtonDown("Quit Controller"))
            Application.Quit();
        
        // This is currently just for testing to see if the variables remain the same between scenes. DELETE THIS ONCE ACTUAL LEVELS ARE IMPLEMENTED
        //if (Input.GetKeyDown(KeyCode.M))
            //if (SceneManager.GetActiveScene().name == "Game")
                //SceneManager.LoadScene("Game2");
            //else
                //SceneManager.LoadScene("Game");

        // This makes sure that the player's health, mana, and ammo don't exceed their maximum or minimum values.
        CapPlayerStatistics();

        // This updates the ammo, mana, and health GUI to match the actual values for ammo and health.
        UpdateGUI();
        
        // Don't forget to come back here and add Controller support once you figure it out
        if (Input.GetButtonDown("Sword Attack Keyboard") || Input.GetButtonDown("Sword Attack Controller") && !isSwinging)
        {
            //Destroy(Instantiate(sword, swordStartPosition, swordStartPosition), 2f);
            StartCoroutine("SwingSword");
        }

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
        justStarted = false;
    }

    // This updates the ammo, mana, and health GUI to match the actual values for ammo and health.
    public void UpdateGUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo : " + ammo + "\n" + "Mana : " + mana + "\n" + "Health : " + health;
        }
    }

    // This makes sure that the player's health, mana, and ammo don't exceed their maximum or minimum values.
    public void CapPlayerStatistics()
    {
        if (mana > manaMax)
            mana = manaMax;
        if (ammo > ammoMax)
            ammo = ammoMax;
        if (health > healthMax)
            health = healthMax;
        if (mana < 0)
            mana = 0;
        if (ammo < 0)
            ammo = 0;
        if (health < 0)
            health = 0;
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

    public void CheckIfPlayerHasCuredAllSlimes()
    {
        GameObject temp = GameObject.FindWithTag("BasicEnemy");

        if ( temp != null)
        {
            playerCuredAllEnemies = false;
        }
        else
        {
            playerCuredAllEnemies = true;
        }
    }

    public void CheckIfPlayerDead()
    {
        if (health <= 0)
            playerIsDead = true;
    }

    public void CheckIfGameOver()
    {
        if ( playerIsDead )
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Defeat");
        }
        else if ( playerCuredAllEnemies )
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Victory");
        }
    }

    public bool getGameOver()
    {
        return gameOver;
    }
    public bool GetIsSwinging()
    {
        return isSwinging;
    }

    // GetMana() returns the "mana" variable.
    public int GetMana()
    {
        return mana;
    }

    // GetHealth() returns the "health" variable.
    public int GetHealth()
    {
        return health;
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

    // This changes the ammo to the current ammo plus whatever value is passed in the parameter.
    public void SetAmmo(int value)
    {
        ammo += value;
    }

    // This changes the mana to the current mana plus whatever value is passed in the parameter.
    public void SetMana(int value)
    {
        mana += value;
    }

    // This changes the mana to the current mana plus whatever value is passed in the parameter.
    public void SetHealth(int value)
    {
        health += value;
    }

    // GetRegenTimer() returns the "regenTimer" variable.
    public float GetRegenTimer()
    {
        return regenTimer;
    }

    // This changes the variables associated with the player "swinging" their sword. It basically tells the game that the player is swinging and needs to wait to swing
    // again.
    public IEnumerator SwingSword()
    {
        GetComponent<AudioSource>().Play();
        isSwinging = true;
        anim.SetInteger("State", 1);
        if (!sword.activeSelf)
            sword.SetActive(true);
        if (bow.activeSelf)
            bow.SetActive(false);
        yield return new WaitForSeconds(swordCooldown);
        anim.SetInteger("State", 0);
        isSwinging = false;
    }

    // This changes the variables associated with the player "shooting" their bow. It basically tells the game that the player is shooting their bow and needs to wait 
    // to shoot again.
    public void ShootBow()
    {
        // Figure out how to make this only play when the arrow shoots and not every time the button is pressed
        bow_SFX.Play();
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