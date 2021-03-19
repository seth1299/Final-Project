using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicEnemyController : MonoBehaviour
{
    [Tooltip("This is how much health the enemy has.")]
    public int health;
    [Tooltip("This is the text that the health will be displayed on.")]
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void UpdateHealthText()
    {
        if (health > 0)
            healthText.text = "Health: " + health;
        else
        {
            healthText.fontSize = 0.4f;
            healthText.text = "Now's your chance, heal them with your magical ability!";
        }
    }
}
