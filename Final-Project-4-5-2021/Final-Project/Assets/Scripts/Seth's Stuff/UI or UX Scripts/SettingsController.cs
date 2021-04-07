using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public AudioMixer audioMixer;
    
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
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
