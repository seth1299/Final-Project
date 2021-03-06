using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Tooltip("This is how sensitive the mouse is when moving the mouse around. 100f is generally the accepted value for this variable.")]
    public float mouseSensitivity = 100f;

    [Tooltip("This is the player's game object.")]
    public Transform playerBody;

    private float xRotation = 0f;

    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (!isPaused)
        {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp (xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
        }

        if ( ( Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller")) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }
        else if ( ( Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller")) && isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }


    }

    // This just returns the "mouseSensitivity" variable.
    public float GetSensivity()
    {
        return mouseSensitivity;
    }

    // This just sets the "mouseSensitivity" variable to a new value.

    public void SetSensitivity(float val)
    {
        mouseSensitivity = val;
    }
}