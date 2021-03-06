using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dummy : MonoBehaviour
{
    [Tooltip("This is how much health the enemy has.")]
    public int health;
    [Tooltip("This is the text that the health will be displayed on.")]
    public TextMeshProUGUI healthText;

    [Tooltip("This is the Game Controller's game object.")]
    public GameObject gameController;

    [Tooltip("This is the Tutorial Manager game object that will be handling the tutorial.")]
    public GameObject tutorialManager;

    public GameObject ps;

    private bool healed = false, canBeHit = true;

    private GameObject levelController;

    void Awake()
    {
        ps.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelController = GameObject.FindWithTag("LevelController");
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
    }

    // Update is called once per frame
    void Update()
    {
        if (levelController.GetComponent<HasClearedLevelController>().GetTutorial())
            Destroy(gameObject);
        else
        {
        UpdateHealthText();
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
        canBeHit = false;

        // This subtracts one from the enemy's current health and updates their health text accordingly.
        health--;

        UpdateHealthText();

        for (int i = 0; i < 60; i++)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
            yield return null;
            yield return null;
        }

        canBeHit = true;

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
