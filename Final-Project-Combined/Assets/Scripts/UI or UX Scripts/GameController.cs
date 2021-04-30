using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    // "isPaused" checks to see if the game is paused or not. "clearedTutorial" checks to see if the player has already cleared the tutorial.
    private bool gameOver = false, isSwinging = false, isPaused = false, playerCuredAllEnemies = false, playerIsDead = false, justStarted = true, clearedTutorial;

    [Tooltip("This is how much mximum ammo the player has.")]
    public int ammoMax;

    [Tooltip("This is the maximum amount of health that the player can have.")]
    public int healthMax;

    // "ammo" is how much ammo the player has currently, "mana" is how much mana the player has currently, and "health" is how much health the player has currently.
    private int ammo, mana, health = 1;

    [Tooltip("This is how much maximum mana the player has.")]
    public int manaMax;

    [Tooltip("This is the animator component of the player's Sword.")]
    public Animator anim;

    [Tooltip("This is the sound effect that the bow plays when shot.")]
    public AudioSource bow_SFX;

    [Tooltip("This is the sound effect that the magic plays when cast.")]
    public AudioSource magic_SFX;

    [Tooltip("This is how long the bow takes to cool down.")]
    public float bowShotCooldown;

    [Tooltip("This is how long the magic takes to cool down.")]
    public float magicCooldown;

    // These are the time remaining for each mechanic. Basically, when you swing your sword, shoot your bow, or use your magic, this is how long until 
    // they are cooled down and ready to use again.
    private float bowShotCooldownTimeRemaining, swordCooldownTimeRemaining, magicCooldownTimeRemaining;

    [Tooltip("This is the player's game object.")]
    public GameObject player;

    [Tooltip("This is the UI for the controls displayed on the game screen.")]
    public GameObject[] controlsUI;

    // These are the sliders for the UI elements.
    public Slider healthBar, manaBar, swordUI, arrowUI, magicUI, ammoUI;

    // These are the gradients for the UI elements.
    public Gradient healthBarGradient, manaBarGradient;

    // These are the "fill" images for the UI elements.
    public Image healthBarFill, manaBarFill, swordFill, arrowFill, magicFill, ammoFill;

    [Tooltip("This is for displaying the name of the current level.")]
    public TextMeshProUGUI currentLevelText;

    // This just checks how many controllers are active.
    private int joystickNamesLength = 0;

    void Awake()
    {
        healthBar.maxValue = healthMax;
        healthBar.value = healthMax;
        healthBarFill.color = healthBarGradient.Evaluate(1f);
        manaBar.maxValue = manaMax;
        manaBar.value = manaMax;
        manaBarFill.color = manaBarGradient.Evaluate(1f);
        swordUI.maxValue = swordCooldown;
        arrowUI.maxValue = bowShotCooldown;
        magicUI.maxValue = magicCooldown;
        ammoUI.maxValue = ammoMax;
        ammoUI.value = ammoMax;
        StartCoroutine("CheckForGameOver");
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ( SceneManager.GetActiveScene().name == "Terrain Builder" )
        {
            currentLevelText.text = "Current Level: Magician's Meadow";
        }
        else if ( SceneManager.GetActiveScene().name == "TutorialReal" )
        {
            currentLevelText.text = "Current Level: Tutorial";
        }
        else
        {
            currentLevelText.text = "Current Level: " + SceneManager.GetActiveScene().name;
        }
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
        joystickNamesLength = Input.GetJoystickNames().Length;
    }
    void Update()
    {    
        //Debug.Log(playerIsDead + ", " + playerCuredAllEnemies);

        //Debug.Log(( SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "Defeat"));

        string name = SceneManager.GetActiveScene().name;

        HasClearedLevelController hclc = null;

        if ( GameObject.FindWithTag("LevelController") != null )
            hclc = GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>();

        //if ( ( !playerIsDead && ( !playerCuredAllEnemies || SceneManager.GetActiveScene().name == "TutorialReal" ) ) && ( SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "Defeat") )
        
        //if ( ( hclc.GetFirstLevel() == false && name == "Terrain Builder" ) || ( hclc.GetSecondLevel() == false && name == "Suspicious Sands" ) || ( hclc.GetThirdLevel() == false && name == "Powdery Peaks" ) )
        //{

        if ( hclc != null )
        {
            if ( hclc.GetPlayerInvincibility())
            {
                SetHealth(healthMax);
            }
            if ( hclc.GetPlayerInfiniteAmmo())
            {
                SetAmmo(ammoMax);
            }
            if ( hclc.GetPlayerInfiniteMana())
            {
                SetMana(manaMax);
            }
        }

        //Debug.Log("Working");

        // This checks if the player is pressing the button to pause the game.
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
            isPaused = !isPaused;
        
        // This makes sure that the game controller isn't updating game variables while the game is paused.
        if (!isPaused)
        {

        if (Input.GetButtonDown("Quit Keyboard") || Input.GetButtonDown("Quit Controller"))
            Application.Quit();

        // This makes sure that the player's health, mana, and ammo don't exceed their maximum or minimum values.
        CapPlayerStatistics();

        // This updates the ammo, mana, and health GUI to match the actual values for ammo and health.
        UpdateGUI();

        // Don't forget to come back here and add Controller support once you figure it out
        if (Input.GetButtonDown("Sword Attack Keyboard") || Input.GetButtonDown("Sword Attack Controller") && !isSwinging)
        {
            StartCoroutine("SwingSword");
        }

        // This is for shooting the player's bow.
        if (Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") && !isSwinging)
        {
            ShootBow();
        }

        // This is for unequiping everything.
        if (Input.GetButtonDown("Unequip Everything Keyboard") || Input.GetButtonDown("Unequip Everything Controller") && !isSwinging)
        {
            UnequipEverything();
        }

        RegenerateMana();
        //}
        }
        justStarted = false;
    }

    void LateUpdate()
    {
        //if ( ( !playerIsDead && ( !playerCuredAllEnemies || SceneManager.GetActiveScene().name == "TutorialReal" ) ) && ( SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "Defeat") )
        //{
            // This is for doing the cure ability.
            if (Input.GetButtonDown("Cure Ability Keyboard") || Input.GetButtonDown("Cure Ability Controller") && !isSwinging)
            {
                DoMagic();
            }
        //}
    }

    // This updates the ammo, mana, and health GUI to match the actual values for ammo and health.
    public void UpdateGUI()
    {

        // This makes the UI controls automatically update based on if a controller is plugged in or not.
        if (joystickNamesLength > 0)
        {
            controlsUI[0].SetActive(false);
            controlsUI[1].SetActive(false);
            controlsUI[2].SetActive(false);
            controlsUI[3].SetActive(true);
            controlsUI[4].SetActive(true);
            controlsUI[5].SetActive(true);
        }
        else
        {
            controlsUI[0].SetActive(true);
            controlsUI[1].SetActive(true);
            controlsUI[2].SetActive(true);
            controlsUI[3].SetActive(false);
            controlsUI[4].SetActive(false);
            controlsUI[5].SetActive(false);
        }

        if ( isSwinging && swordCooldownTimeRemaining <= 0 )
            swordCooldownTimeRemaining = swordCooldown;

        if ( isSwinging )
            swordCooldownTimeRemaining -= Time.deltaTime;

        if ( swordCooldownTimeRemaining <= 0 )
            swordCooldownTimeRemaining = 0;
        
        manaBar.value = mana;
        manaBarFill.color = manaBarGradient.Evaluate(manaBar.normalizedValue);
        swordUI.value = swordCooldownTimeRemaining;
        

        if (bowShotCooldown != 0)
            bowShotCooldownTimeRemaining -= Time.deltaTime;

        if (bowShotCooldownTimeRemaining < 0)
            bowShotCooldownTimeRemaining = 0;

        if (magicCooldown != 0)
        {
            magicCooldownTimeRemaining -= Time.deltaTime;
        }

        if (magicCooldownTimeRemaining < 0)
            magicCooldownTimeRemaining = 0;

        arrowUI.value = bowShotCooldownTimeRemaining;
        magicUI.value = magicCooldownTimeRemaining;
        ammoUI.value = ammo;
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
        GameObject temp2 = GameObject.FindWithTag("Dummy");

        if ( ( temp != null || temp2 != null ) && !playerCuredAllEnemies)
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

            if ( SceneManager.GetActiveScene().name == "Terrain Builder" )
            {
                SceneManager.LoadScene("Terrain Builder");
            }
            else if ( SceneManager.GetActiveScene().name == "Powdery Peaks" )
            {
                SceneManager.LoadScene("Powdery Peaks");
            }
            else if ( SceneManager.GetActiveScene().name == "Suspicious Sands" )
            {
                SceneManager.LoadScene("Suspicious Sands");
            }

        }
        /*
        else if ( playerCuredAllEnemies && SceneManager.GetActiveScene().name != "TutorialReal" && SceneManager.GetActiveScene().name != "Suspicious Sands")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TutorialReal");
        }
        */
        /*
        else if ( playerCuredAllEnemies && SceneManager.GetActiveScene().name == "Suspicious Sands" && GameObject.Find("CatPendent") == null )
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TutorialReal");
        }
        */
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

    // GetHealthMax() returns the "health" variable.
    public int GetHealthMax()
    {
        return healthMax;
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

    // GetAmmoMax() returns the "ammoMax" variable.
    public int GetAmmoMax()
    {
        return ammoMax;
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
        manaBar.value = mana;
        manaBarFill.color = manaBarGradient.Evaluate(manaBar.normalizedValue);
    }

    // This changes the mana to the current mana plus whatever value is passed in the parameter.
    public void SetHealth(int value)
    {
        health += value;
        healthBar.value = health;
        healthBarFill.color = healthBarGradient.Evaluate(healthBar.normalizedValue);
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
        if (!isSwinging)
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
        yield return null;
        }
    }

    // This changes the variables associated with the player "shooting" their bow. It basically tells the game that the player is shooting their bow and needs to wait 
    // to shoot again.
    public void ShootBow()
    {
        if ( bowShotCooldownTimeRemaining == 0 && GetAmmo() > 0 && player.GetComponent<PlayerController>().GetAiming())
        {
            bowShotCooldownTimeRemaining = bowShotCooldown;
            bow_SFX.Play();
        }
        // Figure out how to make this only play when the arrow shoots and not every time the button is pressed
        
        if (sword.activeSelf)
            sword.SetActive(false);
        if (!bow.activeSelf)
            bow.SetActive(true);
    }
    public void DoMagic()
    {

        Debug.Log(magicCooldownTimeRemaining + ", " + GetMana() + ", " + player.GetComponent<PlayerController>().GetAiming());

        if ( magicCooldownTimeRemaining == 0 && GetMana() >= 5 && player.GetComponent<PlayerController>().GetAiming())
        {
            SetMana(-5);
            magicCooldownTimeRemaining = magicCooldown;
            magic_SFX.Play();
        }

        if (sword.activeSelf)
            sword.SetActive(false);
        if (bow.activeSelf)
            bow.SetActive(false);
    }

    public void UnequipEverything()
    {
        if (sword.activeSelf)
            sword.SetActive(false);
        if (bow.activeSelf)
            bow.SetActive(false);
    }

}