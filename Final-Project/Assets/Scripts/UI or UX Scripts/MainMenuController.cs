using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuController : MonoBehaviour
{

    // "menuSelector" is an int that selects which menu the player is on.
    private int menuSelector;

    [Tooltip("'mainMenuCanvas' is the Canvas for the Main Menu. 'aboutMenuCanvas' is the Canvas for the About Menu inside of the Main Menu.")]
    public Canvas mainMenuCanvas, aboutMenuCanvas;

    private static GameObject hasClearedLevelController;

    void Start()
    {
        hasClearedLevelController = GameObject.FindWithTag("LevelController");
        menuSelector = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    // The StartGame() function loads the "Game" scene, which should be the actual game content.
    public void StartGame()
    {
        SceneManager.LoadScene("TutorialReal");
    }

    // The About() function loads the "About" scene, which should contain the instructions for how to play the game.
    public void About()
    {
        menuSelector = 1;
        //SceneManager.LoadScene("About");
    }

    // The MainMenu() function returns the user to the main menu.
    public void MainMenu()
    {
        menuSelector = 0;
        if (SceneManager.GetActiveScene().name == "Victory" || SceneManager.GetActiveScene().name == "Defeat")
            SceneManager.LoadScene("MainMenu");
    }

    // This quits the game when the "exit" button is clicked.

    public void Exit()
    {
        hasClearedLevelController.GetComponent<HasClearedLevelController>().SaveData();
        Application.Quit();
    }
    public static void DeleteData()
    {
        string path = Application.persistentDataPath + SaveSystem.filePathName;
        if (File.Exists(path))
        {
                File.Delete(path);
                Debug.Log("Successfully deleted the file path " + path);
        }
        else
        {
            Debug.LogError("Could not delete the file path " + path);
        }
        
        hasClearedLevelController.GetComponent<HasClearedLevelController>().ResetValues();
    }

    void Update()
    {
        // This code simply closes the application when the "ESC" key is pressed, as required in the "requirements for ALL games" section on Webcourses.
        if (Input.GetButtonDown("Quit Keyboard") || Input.GetButtonDown("Quit Controller") || Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (menuSelector == 1)
        {
            mainMenuCanvas.enabled = false;
            aboutMenuCanvas.enabled = true;
        }
            
        else
        {
            mainMenuCanvas.enabled = true;
            aboutMenuCanvas.enabled = false;
        }
    }

    public int GetMenuSelector()
    {
        return menuSelector;
    }

}