using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public AudioMixer audioMixer;

    private GameObject levelController;

    private bool isPaused = false, sceneWasJustLoaded;

    public Toggle infiniteHealth, infiniteAmmo, infiniteMana, isFullscreenToggle;

    public TMPro.TMP_Dropdown qualityDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindWithTag("LevelController");
        GetComponent<Canvas>().enabled = false;
        if ( levelController != null )
        {
            //Debug.Log("Setting quality and fullscreen");
            //QualitySettings.SetQualityLevel(GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetQuality());
            //Screen.fullScreen = GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetIsFullscreen();
        }
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
        StartCoroutine("SceneWasJustLoaded");

        HasClearedLevelController hclc = GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>();

        if ( hclc != null && ( SceneManager.GetActiveScene().name != "MainMenu" ) &&  ( SceneManager.GetActiveScene().name != "BEGINNING_SCENE" ) )
        {

        bool isFull = hclc.GetIsFullscreen(), hasInfiniteHealth = hclc.GetPlayerInvincibility(), hasInfiniteAmmo = hclc.GetPlayerInfiniteAmmo(), hasInfiniteMana = hclc.GetPlayerInfiniteMana();
        int quality = hclc.GetQuality();
        
        isFullscreenToggle.isOn = isFull;
        infiniteHealth.isOn = hasInfiniteHealth;
        infiniteAmmo.isOn = hasInfiniteAmmo;
        infiniteMana.isOn = hasInfiniteMana;
        //QualitySettings.SetQualityLevel(quality);
        hclc.SetQuality(quality);
        qualityDropdown.value = quality;

        }
    }

    public IEnumerator SceneWasJustLoaded()
    {
        sceneWasJustLoaded = true;
        yield return new WaitForSeconds(0.1f);
        sceneWasJustLoaded = false;
        yield return null;
    }

    
    void LateUpdate()
    {

        //Debug.Log(GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetIsFullscreen());

        if ( GameObject.FindWithTag("LevelController") != null )
        {
            /*
            HasClearedLevelController hclc = GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>();

            bool isFull = hclc.GetIsFullscreen(), hasInfiniteHealth = hclc.GetPlayerInvincibility(), hasInfiniteAmmo = hclc.GetPlayerInfiniteAmmo(), hasInfiniteMana = hclc.GetPlayerInfiniteMana();
            int quality = hclc.GetQuality();

            if (isFull == true)
            {
                Debug.Log("Game is fullscreen");
            }
            else if (isFull == false)
            {
                Debug.Log("Game is not fullscreen");
            }
            */

            /*
            QualitySettings.SetQualityLevel(GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetQuality());

            if ( GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetIsFullscreen())
            {
                //Debug.Log("Setting to true");
                //isFullscreen.isOn = true;
            }
            else
            {
                //Debug.Log("Setting to false");
                //isFullscreen.isOn = false;
            }

            if ( GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetPlayerInvincibility())
            {
                infiniteHealth.isOn = true;
            }
            else
            {
                infiniteHealth.isOn = false;
            }

            if ( GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetPlayerInfiniteAmmo())
            {
                infiniteAmmo.isOn = true;
            }
            else
            {
                infiniteAmmo.isOn = false;
            }

            if ( GameObject.FindWithTag("LevelController").GetComponent<HasClearedLevelController>().GetPlayerInfiniteMana())
            {
                infiniteMana.isOn = true;
            }
            else
            {
                infiniteMana.isOn = false;
            }
            */
        }

        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
            isPaused = !isPaused;

        if ( isPaused )
        {
            
        }
        else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }

    
    public void ReturnToPauseMenu()
    {
        GetComponent<Canvas>().enabled = false;
        pauseMenuCanvas.GetComponent<PauseMenuController>().SetAbout(false);
        pauseMenuCanvas.GetComponent<PauseMenuController>().SetSettings(false);
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality ()
    {
        if ( levelController != null )
        {
            levelController.GetComponent<HasClearedLevelController>().SetQuality(qualityDropdown.value);

        Debug.Log("Quality is now " + levelController.GetComponent<HasClearedLevelController>().GetQuality());
        }

        //QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool fullscreenParam)
    {
        if ( levelController != null && !sceneWasJustLoaded )
            levelController.GetComponent<HasClearedLevelController>().ToggleFullscreen();

        //Screen.fullScreen = fullscreenParam;
    }

    public void TogglePlayerInfiniteAmmo()
    {
        if ( levelController != null )
            levelController.GetComponent<HasClearedLevelController>().TogglePlayerInfiniteAmmo();
    }

    public void TogglePlayerInfiniteMana()
    {
        if ( levelController != null )
            levelController.GetComponent<HasClearedLevelController>().TogglePlayerInfiniteMana();
    }

    public void TogglePlayerInvincibility()
    {
        if ( levelController != null )
            levelController.GetComponent<HasClearedLevelController>().TogglePlayerInvincibility();
    }

}
