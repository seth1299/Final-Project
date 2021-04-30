using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutMenuController : MonoBehaviour
{
    [Tooltip("'pauseMenuCanvas' is the pauseMenuCanvas object that you want to control. In this case, it's the entire 'Pause Menu' pauseMenuCanvas since we want to reference those functions later.")]
    public Canvas pauseMenuCanvas, settingsCanvas;
    [Tooltip("'thisSameCanvas' is the same canvas that this script is on.")]
    public Canvas thisSameCanvas;

    // "PMC" is a PauseMenuController script, referenced from another pauseMenuCanvas object (whatever you passed to the "pauseMenuCanvas" object earlier in this script)
    private PauseMenuController PMC;

    [Tooltip("'isInAboutMenu' tracks if the player is in the 'About' menu or not. 'true' if the player is in the 'About' menu, or 'false' otherwise.")]
    public bool isInAboutMenu = false, isInSettingsMenu = false;

    [Tooltip("This is the Image of Xbox Controls.")]
    public Image Xbox_Controls;

    [Tooltip("This is the Image of PC Controls.")]
    public Image PC_Controls;

    private int joystickNamesLength;

    // Start() is called once at the beginning of the Scene load.
    void Start()
    {
        // "PMC" is set to the PauseMenuController script from the "pauseMenuCanvas" object that was passed to this script earlier.
        PMC = pauseMenuCanvas.GetComponent<PauseMenuController>();

        // This disables the "about" menu at the start of the game so that it doesn't show up on accident.
        thisSameCanvas.enabled = false;

        joystickNamesLength = Input.GetJoystickNames().Length;
    }

    // Update is called once per frame.
    void Update()
    {
        if ( SceneManager.GetActiveScene().name != "MainMenu" )
        {
        // "isInAboutMenu" is set to the variable returned by the "GetIsInAboutMenu()" function from the "PauseMenuController" script that "PMC" is referencing. 
        // In short, this is just true if the player is in the "about" menu or false otherwise.
        isInAboutMenu = PMC.GetIsInAboutMenu();
        isInSettingsMenu = PMC.GetIsInSettingsMenu();
        }
        
        // This makes the About Menu visible if the player is in the about menu, otherwise it makes it invisible.
        if ( isInAboutMenu )
        {
            thisSameCanvas.enabled = true;
            if ( joystickNamesLength > 0 )
            {
                Xbox_Controls.enabled = true;
                PC_Controls.enabled = false;
            }
            else
            {
                Xbox_Controls.enabled = false;
                PC_Controls.enabled = true;
            }
        if (settingsCanvas != null)
            settingsCanvas.enabled = false;
        }
        else if ( isInSettingsMenu )
        {
        if (settingsCanvas != null)
            settingsCanvas.enabled = true;
        thisSameCanvas.enabled = false;
        }
        else
            thisSameCanvas.enabled = false;
    }

    public void ReturnToPauseMenu()
    {
        PMC.SetAbout(false);
        PMC.SetSettings(false);
    }
}