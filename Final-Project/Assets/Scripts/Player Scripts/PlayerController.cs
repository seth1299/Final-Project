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
    public bool aiming;
    public GameObject projectile1;
    public GameObject projectile2;
    public Camera thirdPersonCam;
    public Camera firstPersonCam;
    public Text UI;
    private int ammo;
    private int mana;
    private int manaMax;
    private float regenTimer;
    private Vector3 moveDirect;
    void Start()
    {
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
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        transform.Rotate(new Vector3 (0, Input.GetAxis("Mouse X"), 0), Space.Self);
        moveDirect = transform.right * x + transform.forward * z;

        UI.text = "Ammo : " + ammo + "\n" + "Mana : " + mana;
        
        if (Input.GetMouseButtonDown(1))
        {
            CameraSwitch();
        }

        if (Input.GetKeyDown(KeyCode.E) && aiming == true|| Input.GetKeyDown(KeyCode.R) && aiming == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ammo > 0)
                {
                    GameObject projectileShot = Instantiate(projectile1, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    projectileActive.Launch(gameObject);
                    ammo--;
                }
            }
            else 
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
    void FixedUpdate() // Physics is calculated when fixedupdate runs, movement here is consistent between the editor and builds.
    {
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
