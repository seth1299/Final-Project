/* Made by Fisher Hensley and Seth Grimes for Nine Lives Studio
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
    
    [HideInInspector]
    public bool aiming;

    public bool powerUp;

    private float speedVal,stunTime,powerTime;
    public GameObject projectile1;

    [Tooltip("This is the magical curing projectile that the player shoots.")]
    public GameObject projectile2;

    [Tooltip("This is the camera that will be used for the third person view.")]
    public Camera thirdPersonCam;

    [Tooltip("This is the camera that will be used for the first person view.")]
    public Camera firstPersonCam;

    [Tooltip("This is the canvas that the crosshair is on.")]
    public Canvas crosshairCanvas;

    [Tooltip("This is how long it takes between bow shots.")]
    public float bowShotCooldown;

    // This is how much time is remaining on the current bow shot cooldown.
    private float bowShotCooldownTimeRemaining;
    
    // This is how much ammo the player has.
    private int ammo;

    // This is how much mana the player has.
    private int mana;

    // This is the maximum amount of mana that the player can reach.
    private int manaMax;

    // This is how long the player must wait before regenerating mana. Negative values are faster, positive values are slower.
    private float regenTimer;

    // This is the move direction of the player.
    private Vector3 moveDirect;

    // This tracks if the game is paused or not.
    private bool isPaused = false;

    public GameObject GameController;
    private GameController gc;

    private bool touchingSolid;

    public Animator anim;

    void Awake()
    {
        gc = GameController.GetComponent<GameController>();
        crosshairCanvas.enabled = false;
        
    }

    void Start()
    {
        regenTimer = gc.GetRegenTimer();

        manaMax = gc.GetManaMax();

        Cursor.lockState = CursorLockMode.Locked;

        manaMax = mana;

        thirdPersonCam.enabled = true;

        firstPersonCam.enabled = false;

        aiming = false;

        speedVal = speed;

        powerUp = false;

        powerTime = 10.0f;

        stunTime = 5.0f;

    }

    void Update()
    {
        if (bowShotCooldown != 0)
            bowShotCooldownTimeRemaining -= Time.deltaTime;

        if (bowShotCooldownTimeRemaining < 0)
            bowShotCooldownTimeRemaining = 0;

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
            CameraSwitch();
        }

        if ( ( ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") )  && aiming == true ) || ( ( Input.GetButtonDown("Cure Ability Keyboard") || Input.GetButtonDown("Cure Ability Controller") ) && aiming == true))
        {

            if ( Input.GetButtonDown("Ranged Attack Keyboard") || Input.GetButtonDown("Ranged Attack Controller") && aiming )
            {

                if (ammo > 0 && aiming && bowShotCooldownTimeRemaining == 0)
                {
                    bowShotCooldownTimeRemaining = bowShotCooldown;
                    
                    gc.SetAmmo(-1);
                    
                    GameObject projectileShot = Instantiate(projectile1, transform.position + transform.forward, Quaternion.identity);
                    
                    projectileShot.transform.Rotate(80f, 180f, 150f);
                    
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    
                    projectileActive.Launch(gameObject);
                }

            }

            else if (aiming)
            {

                if (mana >= 5 && aiming)
                {
                    Debug.Log("Subtracting 5 from mana");
                    gc.SetMana(-5);
                    
                    GameObject projectileShot = Instantiate(projectile2, transform.position + transform.forward, Quaternion.Euler(new Vector3(0, transform.rotation.x, 0)));
                    
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    
                    projectileActive.Launch(gameObject);
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

        if (!touchingSolid)
        {
            controller.Move(Vector3.down * Time.deltaTime);
        }

        }
    }
    void FixedUpdate() // Physics is calculated when fixedupdate runs, movement here is consistent between the editor and builds.
    {
        if (!isPaused)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            anim.SetInteger("State", 0);
            else
            anim.SetInteger("State", 1);
            controller.Move(moveDirect * speed * Time.deltaTime);
        }
    }
    public bool GetAiming()
    {
        return aiming;
    }

    
    void CameraSwitch() // This controls what camera the player is using.
    {
        thirdPersonCam.enabled = !thirdPersonCam.enabled;

        firstPersonCam.enabled = !firstPersonCam.enabled;
        
        if (crosshairCanvas.enabled)
        {
            Debug.Log("Disabling crosshair canvas");

            crosshairCanvas.enabled = false;
        }

        else
        {
            crosshairCanvas.enabled = true;
        }

        aiming = !aiming;
    }
    void OnCollisionEnter(Collision entered)
    {
        if (entered == null)
        {
            
        }

        else if (entered.gameObject.tag == "Ammunition")
        {
            ammo++;
            Destroy(entered.gameObject);
        }
        else if (entered.gameObject.tag == "BasicEnemy" && this.gameObject.tag != "Sword")
        {
            Debug.Log("Touching enemy");

            gc.SetHealth(-1);

            StartCoroutine("Knockback");
        }
        else if (entered.gameObject.tag == "Solid" || entered.gameObject.tag == "Terrain")
        {
            touchingSolid = true;
        }
        else if (entered.gameObject.tag == "PowerUp")
        {
            StartCoroutine("PowerupTime");
            Destroy(entered.gameObject);
        }
        else
        {

        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Solid")
        {
            touchingSolid = false;
        }
    }

    public IEnumerator Knockback()
    {
        anim.SetBool("Hit", true);
        for (int i = 0; i < 120; i++)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
                yield return null;
                yield return null;
            }
        anim.SetBool("Hit", false);
    }
    public void Snare()
    {
        StartCoroutine(Stun());
        gc.SetHealth(-1);
    }

    public void OpenHealthMod(int healthMod)
    {
        gc.SetHealth(healthMod);
    }

    IEnumerator Stun()
    {
        speed = 0;
        yield return new WaitForSecondsRealtime(stunTime);
        speed = speedVal;
    }

    IEnumerator PowerupTime()
    {
        powerUp = true;
        yield return new WaitForSecondsRealtime(powerTime);
        powerUp = false;
    }
}
