using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class HasClearedLevelController : MonoBehaviour
{
    public bool hasBeatenTutorial = false, hasBeatenFirstLevel = false, hasBeatenSecondLevel = false, hasBeatenThirdLevel = false, playerIsInvincible = false, playerHasInfiniteAmmo = false, playerHasInfiniteMana = false, isFullscreen = true;
    
    public int quality;
    private string name = "";

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BEGINNING_SCENE")
        {
            LoadData();
            SceneManager.LoadScene("MainMenu");
        }

        DontDestroyOnLoad(this);
    }
    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        GameData data = SaveSystem.LoadData();
        if (data != null)
        {
        hasBeatenTutorial = data.hasBeatenTutorial;
        hasBeatenFirstLevel = data.hasBeatenFirstLevel;
        hasBeatenSecondLevel = data.hasBeatenSecondLevel;
        hasBeatenThirdLevel = data.hasBeatenThirdLevel;
        playerIsInvincible = data.playerIsInvincible;
        playerHasInfiniteAmmo = data.playerHasInfiniteAmmo;
        playerHasInfiniteMana = data.playerHasInfiniteMana;
        isFullscreen = data.isFullscreen;
        quality = data.quality;
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
        name = scene.name;
        SaveData();
        if ( hasBeatenTutorial && hasBeatenFirstLevel && hasBeatenSecondLevel && hasBeatenThirdLevel && SceneManager.GetActiveScene().name != "Victory" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void TogglePlayerInvincibility()
    {
        playerIsInvincible = !playerIsInvincible;
    }

    public bool GetPlayerInvincibility()
    {
        return playerIsInvincible;
    }

    public void TogglePlayerInfiniteAmmo()
    {
        playerHasInfiniteAmmo = !playerHasInfiniteAmmo;
    }

    public bool GetPlayerInfiniteAmmo()
    {
        return playerHasInfiniteAmmo;
    }

    public void TogglePlayerInfiniteMana()
    {
        playerHasInfiniteMana = !playerHasInfiniteMana;
    }

    public void ToggleFullscreen()
    {
        Debug.Log("isFullscreen value before changing: "+isFullscreen);
        isFullscreen = !isFullscreen;
        Debug.Log("isFullscreen value after changing: "+isFullscreen);
    }

    public bool GetPlayerInfiniteMana()
    {
        return playerHasInfiniteMana;
    }

    public bool GetIsFullscreen()
    {
        return isFullscreen;
    }

    public void SetQuality(int param)
    {
        quality = param;
        QualitySettings.SetQualityLevel(param);
    }

    public int GetQuality()
    {
        return quality;
    }

    void Update()
    {
        string name = SceneManager.GetActiveScene().name;

        GameObject temp = GameObject.FindWithTag("Dummy");
        GameObject temp2 = GameObject.FindWithTag("BasicEnemy");
        GameObject temp3 = GameObject.FindWithTag("TutorialManager");        
        GameObject temp4 = GameObject.FindWithTag("InfectedTree");
        GameObject temp5 = GameObject.Find("CatPendent");

        if (temp == null && temp2 == null)
        {
            if ( name == "TutorialReal" && temp3 == null)
            {
                hasBeatenTutorial = true;
            }
            else if ( name == "Terrain Builder" && temp4 == null )
            {
                hasBeatenFirstLevel = true;
            }
            else if ( name == "Suspicious Sands" && temp5 == null )
            {
                hasBeatenSecondLevel = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("TutorialReal");
            }
            else if ( name == "Powdery Peaks" )
            {
                hasBeatenThirdLevel = true;
            }
        }

        if ( hasBeatenFirstLevel && name == "Terrain Builder" )
        {
            SceneManager.LoadScene("TutorialReal");
        }
        else if ( hasBeatenSecondLevel && name == "Suspicious Sands" )
        {
            SceneManager.LoadScene("TutorialReal");
        }
        else if ( hasBeatenThirdLevel && name == "Powdery Peaks" )
        {
            SceneManager.LoadScene("TutorialReal");
        }
    }

    public bool GetTutorial()
    {
        return hasBeatenTutorial;
    }

    public bool GetFirstLevel()
    {
        return hasBeatenFirstLevel;
    }

    public bool GetSecondLevel()
    {
        return hasBeatenSecondLevel;
    }

    public bool GetThirdLevel()
    {
        return hasBeatenThirdLevel;
    }

    public void ResetValues()
    {
        hasBeatenTutorial = false;
        hasBeatenFirstLevel = false;
        hasBeatenSecondLevel = false;
        hasBeatenThirdLevel = false;
        isFullscreen = true;
        quality = 0;
        playerIsInvincible = false;
        playerHasInfiniteAmmo = false;
        playerHasInfiniteMana = false;
    }

}
