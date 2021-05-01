using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private GameObject levelController, Magicians_Meadow_Fence, Suspicious_Sands_Fence, Powdery_Peaks_Fence;

    void Start()
    {
        if ( levelController != null )
        {

            if ( levelController.GetComponent<HasClearedLevelController>().GetTutorial() == false )
            {
                if ( SceneManager.GetActiveScene().name == "TutorialReal" )
                {
                    /*
                    Magicians_Meadow_Fence = GameObject.Find("Magicians_Meadow_Fence");
                    Suspicious_Sands_Fence = GameObject.Find("Suspicious_Sands_Fence");
                    Powdery_Peaks_Fence = GameObject.Find("Powdery_Peaks_Fence");
                    Magicians_Meadow_Fence.transform.position = new Vector3(198.88f, 1.38f, 68.12f);
                    Suspicious_Sands_Fence.transform.position = new Vector3(198.88f, 1.38f, 95.81f);
                    Powdery_Peaks_Fence.transform.position = new Vector3(198.63f, 1.38f, 129.1f);
                    */
                }
            }

        }
    }
    void Update()
    {
        if ( levelController != null )
        {
        if ( levelController.GetComponent<HasClearedLevelController>().GetTutorial() )
        {
                if ( SceneManager.GetActiveScene().name == "TutorialReal" )
                {
                    
                    if (this.gameObject.name == "Teleporter_Magicians_Meadow")
                        if (levelController.GetComponent<HasClearedLevelController>().GetFirstLevel())
                        {
                            //Magicians_Meadow_Fence.transform.position = new Vector3(198.88f, 1.38f, 68.12f);
                            Destroy(gameObject);
                        }
                        else
                        {
                            //Magicians_Meadow_Fence.transform.position = new Vector3(10000f, 10000f, 10000f);
                            transform.position =  new Vector3 (200.9f, -0.64f, 69.13f);
                        }
                    else if (this.gameObject.name == "Teleporter_Suspicious_Sands")
                        if (levelController.GetComponent<HasClearedLevelController>().GetSecondLevel())
                        {
                            //Suspicious_Sands_Fence.transform.position = new Vector3(198.88f, 1.38f, 95.81f);
                            Destroy(gameObject);
                        }
                        else
                        {
                            //Suspicious_Sands_Fence.transform.position = new Vector3(10000f, 10000f, 10000f);
                            transform.position =  new Vector3 (201.04f, -0.64f, 98.3f);
                        }
                    else if (this.gameObject.name == "Teleporter_Powdery_Peaks")
                        if (levelController.GetComponent<HasClearedLevelController>().GetThirdLevel())
                        {
                            //Powdery_Peaks_Fence.transform.position = new Vector3(198.63f, 1.38f, 129.1f);
                            Destroy(gameObject);
                        }
                        else
                        {
                            //Powdery_Peaks_Fence.transform.position = new Vector3(10000f, 10000f, 10000f);
                            transform.position =  new Vector3 (201.04f, -0.64f, 130.2f);
                        }
                            
                }
                else if ( SceneManager.GetActiveScene().name == "Terrain Builder" )
                    transform.position =  new Vector3 (221.9635f, -0.5799999f, 27.29433f);
                else if ( SceneManager.GetActiveScene().name == "Powdery Peaks")
                    transform.position =  new Vector3 (48.49f, -1.12f, 249.83f);
                else if ( SceneManager.GetActiveScene().name == "Suspicious Sands")
                    transform.position =  new Vector3 (284.72f, -0.32f, 285.69f);
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
                if ( SceneManager.GetActiveScene().name == "TutorialReal" )
                {
                    if (this.gameObject.name == "Teleporter_Magicians_Meadow")
                        SceneManager.LoadScene("Terrain Builder");
                    else if (this.gameObject.name == "Teleporter_Suspicious_Sands")
                        SceneManager.LoadScene("Suspicious Sands");
                    else if (this.gameObject.name == "Teleporter_Powdery_Peaks")
                        SceneManager.LoadScene("Powdery Peaks");
                }
                else
                    SceneManager.LoadScene("TutorialReal");
            }
        }
    }
}
