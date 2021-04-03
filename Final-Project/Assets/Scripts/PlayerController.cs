/* Made by Fisher Hensley for Nine Lives Studio
   and UCF's DIG-4715 Class. Controls player
   movement. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    [HideInInspector]
    public bool aiming;
    public GameObject projectile1;
    public GameObject projectile2;
    public Camera thirdPersonCam;
    public Camera firstPersonCam;
    private int ammo;
    private int mana;
    private int manaMax;
    private float regenTimer;
    private Vector3 moveDirect;
    private bool isPaused = false;

    [Tooltip("This is the GameController game object that this script will reference to get ammo, health, and mana from.")]
    public GameController gc;

    void Start()
    {
        gc = GameController.gc;
        regenTimer = gc.GetRegenTimer();
        manaMax = gc.GetManaMax();
        Cursor.lockState = CursorLockMode.Locked;
        thirdPersonCam.enabled = true;
        firstPersonCam.enabled = false;
        aiming = false;
    }

    void Update()
    {
        ammo = gc.GetAmmo();
        mana = gc.GetMana();
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
            isPaused = !isPaused;
        if (!isPaused)
        {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        transform.Rotate(new Vector3 (0, Input.GetAxis("Mouse X"), 0), Space.Self);
        moveDirect = transform.right * x + transform.forward * z;
        
        if (Input.GetButtonDown("Right Click") || Input.GetButtonDown("Right Click Controller"))
        {
            Debug.Log("Switching Camera");
            CameraSwitch();
        }

        if ( ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") ) && aiming == true || ( Input.GetButtonDown("Cure Ability Keyboard") || Input.GetButtonDown("Cure Ability Controller")) && aiming == true)
        {
            if ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") )
            {
                if (ammo > 0)
                {
                    GameObject projectileShot = Instantiate(projectile1, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    projectileActive.Launch(gameObject);
                    gc.SetAmmo(-1);
                }
            }
            else 
            {
                if (mana >= 5)
                {
                    GameObject projectileShot = Instantiate(projectile2, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    projectileActive.Launch(gameObject);
                    gc.SetMana(-5);
                }
            }
        }

        }
    }
    void FixedUpdate() // Physics is calculated when fixedupdate runs, movement here is consistent between the editor and builds.
    {
        if (!isPaused)
            controller.Move(moveDirect * speed * Time.deltaTime);
    }
    void CameraSwitch() // This controls what camera the player is using.
    {
        thirdPersonCam.enabled = !thirdPersonCam.enabled;
        firstPersonCam.enabled = !firstPersonCam.enabled;
        aiming = !aiming;
    }
    void OnTriggerEnter(Collider entered)
    {
        if (entered.gameObject.tag == "Ammunition")
        {
            gc.SetAmmo(1);
            Destroy(entered.gameObject);
        }
    }
}
