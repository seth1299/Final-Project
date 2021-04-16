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

    [HideInInspector]
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

    // "isPaused" tracks if the game is paused or not, "canBeHit" tracks if the player can be hit, and "touchingQuicksand" tracks if the player is touching Quicksand.
    // "touchingFrozenLake" tracks if the player is touching the frozen lake.
    private bool isPaused = false, canBeHit = true, touchingQuicksand = false, touchingFrozenLake = false;

    public GameObject GameController;
    private GameController gc;

    private bool touchingSolid;

    public Animator anim;

    public Camera cam;

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
        // This handles the bow shot cooldown.
        if (bowShotCooldown != 0)
            bowShotCooldownTimeRemaining -= Time.deltaTime;

        if (bowShotCooldownTimeRemaining < 0)
            bowShotCooldownTimeRemaining = 0;

        // This makes sure that the "ammo" and "mana" variables are set to the GameController's "ammo" and "mana" values.
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

                    projectileShot.transform.rotation = Quaternion.FromToRotation(Vector3.right, transform.forward);
                    
                    //projectileShot.transform.Rotate(80f, 180f, 150f);
                    
                    ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                    
                    projectileActive.Launch(gameObject);
                }

            }

            else if (aiming)
            {

                if (mana >= 5 && aiming)
                {
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

        // This makes sure the player never floats in the air and continuously falls to the ground (if they're not already touching the ground)

        if (!touchingSolid)
        {
            controller.Move(Vector3.down * Time.deltaTime * 3);
        }

        }
    }
    void FixedUpdate() // Physics is calculated when fixedupdate runs, movement here is consistent between the editor and builds.
    {
        if (!isPaused)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                anim.SetInteger("State", 0);
            else if ( !canBeHit )
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
            gc.SetAmmo(1);
            Destroy(entered.gameObject);
        }
        else if (entered.gameObject.tag == "BasicEnemy" && this.gameObject.tag != "Sword" && canBeHit)
        {
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
        else if (entered.gameObject.tag == "Quicksand")
        {
            touchingQuicksand = true;
            StartCoroutine("Quicksand");
        }
        else if (entered.gameObject.tag == "Frozen Lake")
        {
            touchingFrozenLake = true;
            StartCoroutine("FrozenLake");
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
        else if (other.gameObject.tag == "Quicksand")
        {
            touchingQuicksand = false;
        }
        else if (other.gameObject.tag == "Frozen Lake")
        {
            touchingFrozenLake = false;
        }
    }

    public IEnumerator Knockback()
    {
        anim.SetBool("Hit", true);
        canBeHit = false;

        yield return new WaitForSeconds(0.5f);

        /*
        for (int i = 0; i < 120; i++)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
                yield return null;
                yield return null;
            }
        */
        canBeHit = true;
        anim.SetBool("Hit", false);
        yield return null;
    }

    public IEnumerator Quicksand()
    {
        float tempSpeed = speed, tempMouseLook = cam.GetComponent<MouseLook>().GetSensivity();

        cam.GetComponent<MouseLook>().SetSensitivity(tempMouseLook / 40);

        speed *= 0.25f;

        while ( touchingQuicksand && !powerUp )
        {
            yield return null;
        }

        cam.GetComponent<MouseLook>().SetSensitivity(tempMouseLook);

        speed = tempSpeed;

        yield return null;
    }

    public IEnumerator FrozenLake()
    {
        float tempSpeed = speed, tempMouseLook = cam.GetComponent<MouseLook>().GetSensivity();

        cam.GetComponent<MouseLook>().SetSensitivity(tempMouseLook * 6);

        speed *= 3f;

        while ( touchingFrozenLake && !powerUp )
        {
            yield return null;
        }

        speed = tempSpeed;

        cam.GetComponent<MouseLook>().SetSensitivity(tempMouseLook);

        yield return null;
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
