using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{

    [Tooltip("'pauseMenuCanvas' is the 'Pause Menu' canvas. 'settingsMenuCanvas' is the 'Settings Menu' canvas.")]
    public Canvas pauseMenuCanvas, settingsMenuCanvas;

    // "isPaused" keeps track of if the game is paused or not. "true" if the game is paused, otherwise it is "false".
    private bool isPaused = false;

    // "about" keeps track of if the player is on the "about" screen or not. "true" if the player is on the pause screen, or "false" otherwise.
    // "settings" keeps track of if the player is in the "setting" screen or not. "true" if the player is on the settings screen, or "false" otherwise.
    private bool about = false, settings = false;

    public Image XBox_Pause, PC_Pause;

    private int joystickNamesLength = 0;

    void Awake()
    {
        joystickNamesLength = Input.GetJoystickNames().Length;
    }

    // Start() is called once at the Scene load.
    void Start()
    {
        // This just sets the "Pause" menu to be invisible when the game starts.
        pauseMenuCanvas.enabled = false;
    }

    // Update is called once per frame.
    void Update()
    {
        if (joystickNamesLength > 0)
        {
            XBox_Pause.enabled = true;
            PC_Pause.enabled = false;
        }
        else
        {
            PC_Pause.enabled = true;
            XBox_Pause.enabled = false;
        }

        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Defeat" && SceneManager.GetActiveScene().name != "Victory")
        {
        // This sets paused to true if it is currently false or vice versa. It also recognizes if the player is in the about menu and closes that out (only if the
        // game is already paused)
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
        {
            if (isPaused == true)
                isPaused = false;
            else if (isPaused == false)
                isPaused = true;
            if ( about )
                about = false;
            if ( settings )
                settings = false;
        }

        // This makes the pause menu "appear" and "disappear" at the appropriate times (when paused or the about menu is clicked)
        if ( isPaused && !about && !settings)
        {
            pauseMenuCanvas.enabled = true;
        }
        else if ( isPaused && !about && settings)
        {
            //settingsMenuCanvas.enabled = true;
        }
        else
        {
            pauseMenuCanvas.enabled = false;
        }
        if ( about || settings)
        {
            pauseMenuCanvas.enabled = false;
        }

        // This sets the time scale to 0 if the game is paused, otherwise sets it to 1. Meaning time doesn't move when paused and it resumes again when unpaused.
        if ( isPaused )
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        }
    }

    // AboutMenu() handles "changing" the "pause" menu to the "about" menu in-game (not in the main menu).
    public void AboutMenu()
    {
        about = true;
        
    }

    public void SettingsMenu()
    {
        settings = true;
    }

    public void Test()
    {
        Debug.Log("Code executed successfully.");
    }

    // GetIsPaused() returns true if the game is paused, otherwise it returns false.
    public bool GetIsPaused()
    {
        return isPaused;
    }

    // GetIsInAboutMenu() returns true if the player is in the About, otherwise it returns false.
    public bool GetIsInAboutMenu()
    {
        return about;
    }

    // GetIsInAboutMenu() returns true if the player is in the About, otherwise it returns false.
    public bool GetIsInSettingsMenu()
    {
        return settings;
    }

    // This just sets the "about" variable to whatever value is passed into the parameter.
    public void SetAbout(bool value)
    {
        about = value;
    }

    // This just sets the "about" variable to whatever value is passed into the parameter.
    public void SetSettings(bool value)
    {
        settings = value;
    }
}