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

    // This 
    private bool touchingSolid = false;

    private bool healed = false;

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
                StartCoroutine("GetHitBySwordForReal");
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
        if (health > 0)
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

        yield return null;
    }

    public void GetHealed()
    {
        healed = true;

        GetComponent<AudioSource>().Play();

        ps.SetActive(true);

        Destroy(gameObject, 0.15f);
    }

    private void UpdateHealthText()
    {
        if (health > 0)
            healthText.text = "Health: " + health;
        else if (health <= 0 && !healed)
        {
            //healthText.fontSize = 0.4f;
            healthText.text = "Now's your chance, heal them with your magical ability!";
        }
        else
            healthText.text = "This enemy is now cured and is saved!";
    }
}
