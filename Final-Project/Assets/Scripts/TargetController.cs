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
        // "rend" is set to the Renderer component on the enemy.
        rend = gameObject.GetComponent<Renderer>();
    }
    
    void OnCollisionEnter(Collision hit)
    {
        // This is when the other object is either the bow shot or the magical curing thing.
        if (hit.gameObject.tag == "Projectile")
        {
            // "type = false" is when the projectile is a bow shot, and "type = true" is when the projectile is the magic shot.
            if (hit.gameObject.GetComponent<ProjectileController>().type == false && health > 0)
            {
                Destroy(hit.gameObject);
                StartCoroutine("GetHitBySwordForReal");
            }
            // This is when the projectile is the magical curing thing.
            else if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == true && health <= 0)
            {
                Destroy(hit.gameObject);
                StartCoroutine("GetHealed");
            }
        }
        // This is when the other object is a sword that is colliding with the enemy.
        else if (hit.gameObject.tag == "Sword")
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
        // This checks to make sure that the enemy is healthy before actually getting hit starts.
        if (health > 0)
            StartCoroutine("GetHitBySwordForReal");
    }

    // This handles the enemy getting hit by the sword.
    public IEnumerator GetHitBySwordForReal()
    {
        // This subtracts one from the enemy's current health and updates their health text accordingly.
        health--;
        UpdateHealthText();

        // This is the code that knocks the enemy back. Originally it was at i < 60, but it has been lowered to i < 30 so it pushes the enemy back half as far now.
        for (int i = 0; i < 30; i++)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
            yield return null;
            yield return null;
        }
        yield return null;
    }

    // This "heals" the enemy once the player sufficiently damages them.
    public void GetHealed()
    {
        healed = true;
        rend.material = material;
    }

    // This updates the health text according to how much health that the enemy has.
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
