using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private GameObject levelController;
    void Update()
    {
        if ( levelController != null )
        {
        if ( levelController.GetComponent<HasClearedLevelController>().GetTutorial() )
        {
                if ( SceneManager.GetActiveScene().name == "Tutorial" )
                    transform.position =  new Vector3 (217.19f, -0.64f, 83.73f);
                else if ( SceneManager.GetActiveScene().name == "Terrain Builder" )
                    transform.position =  new Vector3 (221.9635f, -0.5799999f, 27.29433f);
        }
        else
            transform.position = new Vector3 (10000f, 100000f, 10000f);
        }
        else
            Debug.Log("Level Controller not found");
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
        levelController = GameObject.FindWithTag("LevelController");
    }

/*    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "DontDestroyOnLoad")
        {
            Debug.Log("Scene: " + scene.name + " Loaded!");
        }
    }
*/
    void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if ( SceneManager.GetActiveScene().name == "Tutorial" )
                {
                    if (this.gameObject.name == "Teleporter_Magicians_Meadow")
                        SceneManager.LoadScene("Terrain Builder");
                    else if (this.gameObject.name == "Teleporter_Suspicious_Sands")
                        Debug.Log("Placeholder Suspicious Sands message");
                    else if (this.gameObject.name == "Teleporter_Powdery_Peaks")
                        Debug.Log("Placeholder Powdery Peaks message");
                }
                else
                    SceneManager.LoadScene("Tutorial");
            }
        }
    }
}
