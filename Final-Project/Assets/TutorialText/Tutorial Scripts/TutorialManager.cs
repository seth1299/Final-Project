using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

[Tooltip("This is an array made up of 'pop-up' game objects that will be displayed for the player to read.")]
public GameObject[] keyboardPopups;

[Tooltip("This is an array made up of 'pop-up' game objects that will be displayed for the player to read.")]
public GameObject[] xboxPopups;

[Tooltip("This is the GameController game object.")]
public GameObject gc;

// This counts which pop-up should be currently displayed.
private int popUpIndex, usingXboxController = 0;
private GameObject levelController;

public Canvas pauseMenuCanvas;

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
    levelController = GameObject.FindWithTag("LevelController");
    usingXboxController = Input.GetJoystickNames().Length;
}

void Update ()
{
    if ( !pauseMenuCanvas.GetComponent<PauseMenuController>().GetIsPaused() )
    {

    
    if (levelController.GetComponent<HasClearedLevelController>().GetTutorial())
    {
        if ( usingXboxController == 0 )
        {
            for (int i = 0; i < keyboardPopups.Length; i++) 
            {
                Destroy(keyboardPopups[i]);
            }
        }
        else
        {
            for (int i = 0; i < xboxPopups.Length; i++) 
            {
                Destroy(xboxPopups[i]);
            }
        }
        
        Destroy(gameObject);
    }

        if ( usingXboxController == 0 )
        {
            for (int i = 0; i < keyboardPopups.Length; i++) 
            {
                if ( i == popUpIndex )
                    keyboardPopups[i].SetActive(true);
                else 
                    keyboardPopups[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < xboxPopups.Length; i++) 
            {
                if ( i == popUpIndex )
                    xboxPopups[i].SetActive(true);
                else 
                    xboxPopups[i].SetActive(false);
            }
        }
    
    // This code goes though the GameObject array and sets the array element that's displayed to the correct number.
    /*
    for (int i = 0; i < popUps.Length; i++) 
    {
        if ( i == popUpIndex )
            popUps[i].SetActive(true);
        else 
            popUps[i].SetActive(false);
    }
    */

    // This code changes the index of the pop up whenever the corresponding button is pressed on the keyboard or controller.
    if ( popUpIndex >= 0 && popUpIndex <= 3)
    {
        if ( Input.GetButtonDown("Tutorial Skip Keyboard") || Input.GetButtonDown("Tutorial Skip Controller"))
            popUpIndex++;
    }
    else
    {
        Destroy(gameObject);
    }

    }
}

public int GetPopUpIndex()
{
    return popUpIndex;
}

}
