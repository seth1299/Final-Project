using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    public void ReturnToPauseMenu()
    {
        GetComponent<Canvas>().enabled = false;
        pauseMenuCanvas.GetComponent<PauseMenuController>().SetAbout(false);
        pauseMenuCanvas.GetComponent<PauseMenuController>().SetSettings(false);
    }
}
