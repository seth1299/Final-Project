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
    [Tooltip("This is the Mesh Renderer component attached to the same game object you're putting this script on.")]
    public Renderer rend;
    [Tooltip("This is the material that the target will change to when it's 'cured'.")]
    public Material material;

    private bool healed = false;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }
    
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Projectile")
        {
            if (hit.gameObject.GetComponent<ProjectileController>().type == false && health > 0)
            {
                Debug.Log("Ouchies!");
                Destroy (hit.gameObject);
                StartCoroutine("GetHitBySwordForReal");
                // Code for the enemy getting hit would go here.
            }
            else if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == true && health <= 0)
            {
                Debug.Log("Poof!");
                Destroy (hit.gameObject);
                StartCoroutine("GetHealed");
                // Code for the enemy being pacified would go here.
            }
        }
        else if (hit.gameObject.tag == "Sword")
        {
            if (health > 0)
            {
                Debug.Log("Ouchies sword!");
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
        rend.material = material;
    }

    /*
    public IEnumerator GetHealed()
    {
        Debug.Log("Healing enemy...");
        while (gameObject.GetComponent<Renderer>().material.color.a > 0)
        {
            Debug.Log(rend.material.color.a);
            Color color = rend.material.color;
            float fadeAmount = color.a - (1f * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            rend.material.color = color;
            yield return null;
        }
        Debug.Log("Finished healing enemy...");
    }
    */

    private void UpdateHealthText()
    {
        if (health > 0)
            healthText.text = "Health: " + health;
        else if (health <= 0 && !healed)
        {
            healthText.fontSize = 0.4f;
            healthText.text = "Now's your chance, heal them with your magical ability!";
        }
        else
            healthText.text = "This enemy is now cured and is saved!";
    }
}
