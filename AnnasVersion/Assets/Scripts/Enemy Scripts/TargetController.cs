using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TargetController : MonoBehaviour
{
    [Tooltip("This is how much health the enemy has.")]
    public int health;
    [Tooltip("This is the text that the health will be displayed on.")]
    public TextMeshProUGUI healthText;

    [Tooltip("This is the Game Controller's game object.")]
    public GameObject gameController;

    [Tooltip("This is the particle system that goes 'poof' when the slime dies.")]
    public GameObject ps;

    // This is the game object for the HasClearedLevelController script.
    private GameObject hasClearedLevelController;

    // This is how close the enemy is to the player.
    private float distanceToPlayer;

    // This checks to see if the enemy is touching a solid.
    private bool touchingSolid = false;

    // "healed" checks if the enemy is currently healed or not, "canBeHit" checks if the enemy can be hit.

    private bool healed = false, canBeHit = true;

    public GameObject healthTextLook;

    void Awake()
    {
        hasClearedLevelController = GameObject.FindWithTag("LevelController");
        ps.SetActive(false);
    }
    
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Projectile")
        {
            if (hit.gameObject.GetComponent<ProjectileController>().type == false && health > 0)
            {
                Destroy (hit.gameObject);
                StartCoroutine("GetHitByArrow");
            }
            else if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == true && health <= 0)
            {
                Destroy (hit.gameObject);
                StartCoroutine("GetHealed");
                // Code for the enemy being pacified would go here.
            }
        }
        else if (hit.gameObject.tag == "Sword" && gameController.GetComponent<GameController>().GetIsSwinging())
        {
            if (health > 0)
            {
                StartCoroutine("GetHitBySword");
            }
        }
        else if (hit.gameObject.tag == "Terrain" || hit.gameObject.tag == "Solid")
        {
            touchingSolid = true;
        }
        else if (hit.gameObject.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.tag == "Terrain" || hit.gameObject.tag == "Solid")
        {
            touchingSolid = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerPosition = new Vector3 (GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y, GameObject.FindWithTag("Player").transform.position.z);

        distanceToPlayer = (Vector3.Distance(thisPosition, playerPosition));

        UpdateHealthText();

        if (hasClearedLevelController.GetComponent<HasClearedLevelController>().hasBeatenFirstLevel && SceneManager.GetActiveScene().name == "Terrain Builder")
        {
            Destroy(gameObject);
        }
        else if (hasClearedLevelController.GetComponent<HasClearedLevelController>().hasBeatenSecondLevel && SceneManager.GetActiveScene().name == "Suspicious Sands")
        {
            Destroy(gameObject);
        }
        else if (hasClearedLevelController.GetComponent<HasClearedLevelController>().hasBeatenThirdLevel && SceneManager.GetActiveScene().name == "Powdery Peaks")
        {
            Destroy(gameObject);
        }
    }
    

    public void GetHitBySword()
    {
        if (health > 0 && canBeHit)
            StartCoroutine("GetHitBySwordForReal");
    }

    public IEnumerator GetHitByArrow()
    {
        // This subtracts one from the enemy's current health and updates their health text accordingly.
        health--;
        
        UpdateHealthText();

        
        yield return null;
    }

    public IEnumerator GetHitBySwordForReal()
    {
        if ( canBeHit )
        {

        canBeHit = false;

        // This subtracts one from the enemy's current health and updates their health text accordingly.
        health--;

        UpdateHealthText();

        for (int i = 0; i < 60; i++)
        {
            if ( !touchingSolid )
            {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
            yield return null;
            yield return null;
            }
        }

        canBeHit = true;
        }
        yield return null;
    }

    public void GetHealed()
    {
        healed = true;

        GetComponent<AudioSource>().Play();

        ps.SetActive(true);

        Destroy(gameObject, 0.30f);
    }

    private void UpdateHealthText()
    {
        healthText.transform.LookAt(healthTextLook.transform);
        //healthText.transform.Rotate();
        //healthText.transform.position = new Vector3(healthText.transform.position.x, healthText.transform.position.y, (healthText.transform.position.z * -1));

        if ( distanceToPlayer > 50f )
        {
            healthText.text = "";
        }
        else if (health > 0)
            healthText.text = "Health: " + health;
        else if (health <= 0 && !healed)
        {
            //healthText.fontSize = 0.4f;
            healthText.text = "Cure me, please!";
        }
        else
            healthText.text = "I am cured, yay!";
    }
}
