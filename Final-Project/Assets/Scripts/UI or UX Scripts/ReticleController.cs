using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    [Tooltip("This is the player's game object.")]
    public GameObject player;
    [Tooltip("This is the same canvas that you're putting this script on.")]
    public Canvas thisSameCanvas;

    void Update()
    {
        if (player.GetComponent<PlayerController>().aiming == true)
            thisSameCanvas.enabled = true;
        else
            thisSameCanvas.enabled = false;
    }
}
