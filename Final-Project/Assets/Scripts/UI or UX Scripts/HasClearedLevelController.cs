using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasClearedLevelController : MonoBehaviour
{
    private bool hasBeatenTutorial, hasBeatenFirstLevel, hasBeatenSecondLevel, hasBeatenThirdLevel;
    private string name = "";

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BEGINNING_SCENE")
            SceneManager.LoadScene("MainMenu");
        Debug.Log("Setting tutorial values");
        hasBeatenTutorial = false;
        hasBeatenFirstLevel = false;
        hasBeatenSecondLevel = false;
        hasBeatenThirdLevel = false;
        DontDestroyOnLoad(this);
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

        if (name != "DontDestroyOnLoad")
            Debug.Log(hasBeatenTutorial + ", " +hasBeatenFirstLevel);
    }

    void Update()
    {
        GameObject temp = GameObject.FindWithTag("Dummy");
        GameObject temp2 = GameObject.FindWithTag("BasicEnemy");

        if (temp == null && temp2 == null)
        {
            if ( name == "Tutorial" )
            {
                hasBeatenTutorial = true;
            }
            else if ( name == "Terrain Builder" )
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
        return hasBeatenTutorial;
    }

}
