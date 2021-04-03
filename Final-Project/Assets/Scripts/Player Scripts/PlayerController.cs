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
    [Tooltip("This is the CharacterController component attached to the player character. You can just drag the character onto this.")]
    public CharacterController controller;

    [Tooltip("This is the speed of the player character, or how fast the character can walk.")]
    public float speed;
<<<<<<< Updated upstream
    public bool aiming;
=======
    [HideInInspector]
    public bool aiming = false;

    [Tooltip("This is the arrow that the player's bow shoots.")]
>>>>>>> Stashed changes
    public GameObject projectile1;

    [Tooltip("This is the magical curing projectile that the player shoots.")]
    public GameObject projectile2;

    [Tooltip("This is the camera that will be used for the third person view.")]
    public Camera thirdPersonCam;

    [Tooltip("This is the camera that will be used for the first person view.")]
    public Camera firstPersonCam;
    public Text UI;
    private int ammo;
    private int mana;
    private int manaMax;
    private float regenTimer;
    private Vector3 moveDirect;
    private bool isPaused = false;

    void Start()
    {
<<<<<<< Updated upstream
=======
        gc = GameController.gc;
        regenTimer = gc.GetRegenTimer();
        manaMax = gc.GetManaMax();
>>>>>>> Stashed changes
        Cursor.lockState = CursorLockMode.Locked;
        ammo = 10;
        mana = 20;
        manaMax = mana;
        thirdPersonCam.enabled = true;
        firstPersonCam.enabled = false;
        aiming = false;
        regenTimer = 0;
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.P))
=======
        ammo = gc.GetAmmo();
        mana = gc.GetMana();
        if (Input.GetButtonDown("Pause Keyboard") || Input.GetButtonDown("Pause Controller"))
>>>>>>> Stashed changes
            isPaused = !isPaused;
        if (!isPaused)
        {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        transform.Rotate(new Vector3 (0, Input.GetAxis("Mouse X"), 0), Space.Self);
        moveDirect = transform.right * x + transform.forward * z;

        UI.text = "Ammo : " + ammo + "\n" + "Mana : " + mana;
        
        if (Input.GetButtonDown("Right Click") || Input.GetButtonDown("Right Click Controller"))
        {
            CameraSwitch();
        }

        if ( ( ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") )  && aiming == true ) || ( ( Input.GetButtonDown("Cure Ability Keyboard") || Input.GetButtonDown("Cure Ability Controller") ) && aiming == true))
        {
            if ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") && aiming )
            {
                if (ammo > 0)
                {
                    GameObject projectileShot = Instantiate(projectile1, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    projectileActive.Launch(gameObject);
                    ammo--;
                }
            }
            else if (aiming)
            {
                if (mana >= 5)
                {
                    GameObject projectileShot = Instantiate(projectile2, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    projectileActive.Launch(gameObject);
                    mana -= 5;
                }
            }
        }

        if (mana < manaMax)
        {
            regenTimer += Time.deltaTime;
            {
                if (regenTimer > 2.5)
                {
                    mana++;
                    regenTimer = 0;
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
            ammo++;
            Destroy(entered.gameObject);
        }
    }
}
