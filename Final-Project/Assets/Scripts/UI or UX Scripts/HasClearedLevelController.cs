﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasClearedLevelController : MonoBehaviour
{
    public bool hasBeatenTutorial = false, hasBeatenFirstLevel = false, hasBeatenSecondLevel = false, hasBeatenThirdLevel = false;
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

    void Update()
    {
        GameObject temp = GameObject.FindWithTag("Dummy");
        GameObject temp2 = GameObject.FindWithTag("BasicEnemy");
        GameObject temp3 = GameObject.FindWithTag("TutorialManager");        
        GameObject temp4 = GameObject.FindWithTag("InfectedTree");    

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
            else if ( name == "Suspicious Sands" )
            {
                hasBeatenSecondLevel = true;
            }
            else if ( name == "Powdery Peaks" )
            {
                hasBeatenThirdLevel = true;
            }
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
    }

}
