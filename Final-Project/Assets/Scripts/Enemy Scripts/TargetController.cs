using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    [Tooltip("This is how much health the enemy has.")]
    public int health;
    [Tooltip("This is the text that the health will be displayed on.")]
    public TextMeshProUGUI healthText;

    [Tooltip("This is the Game Controller's game object.")]
    public GameObject gameController;

    public GameObject ps;

    private bool healed = false;

    void Awake()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
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
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
            yield return null;
            yield return null;
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
