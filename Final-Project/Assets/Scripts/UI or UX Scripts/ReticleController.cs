using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject player;
    public Canvas thisSameCanvas;

    void Update()
    {
        if (player.GetComponent<PlayerController>().aiming == true)
            thisSameCanvas.enabled = true;
        else
            thisSameCanvas.enabled = false;
    }
}
