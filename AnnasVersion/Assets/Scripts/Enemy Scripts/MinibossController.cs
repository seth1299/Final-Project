using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class MinibossController : MonoBehaviour
{

    [Tooltip("This is the y-coordinate that the slimes are spawned at.")]
    public float yCord;

    // These are the three enemies and the three navigation points the enemies will follow.
    public GameObject Enemy1, Enemy2, Enemy3, navP1, navP2, navP3, ps;

    // This is how much health the miniboss has.
    private int health;

    // This checks to see if the miniboss is healed or not.
    private bool healed;
    private GameObject currentEnemy, protectiveShield;

    [Tooltip("This is the text that the health will be displayed on.")]
    public TextMeshProUGUI healthText;

    Animator anim;

    // This tracks if the enemy can be hit.
    private bool canBeHit = true;

    // This is how close the enemy is to the player.
    private float distanceToPlayer;

    public GameObject healthTextLook;

    void Start()
    {
        protectiveShield = GameObject.Find("Protective Shield");
        protectiveShield.SetActive(false);
        health = 4;
        healed = false;
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerPosition = new Vector3 (GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y, GameObject.FindWithTag("Player").transform.position.z);

        UpdateHealthText();

        distanceToPlayer = (Vector3.Distance(thisPosition, playerPosition));

        if (currentEnemy == null)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            protectiveShield.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            protectiveShield.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision hit)
    {
        Debug.Log("Hit by " +hit.gameObject.name);
        if (hit.gameObject.tag == "Projectile")
        {
            if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == false)
            {
                Destroy(hit.gameObject);
                health--;
                StartCoroutine("HitAnimation");
                WaveChange();
            }
            else if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == true && health <= 0)
            {
                Destroy (hit.gameObject);
                StartCoroutine("GetHealed");
            }
        }
    }

    public void GetHitBySword()
    {
        if (canBeHit)
        {
            health--;
            StartCoroutine("HitAnimation");
            WaveChange();
        }
    }
    public void GetHealed()
    {
        healed = true;
        GetComponent<AudioSource>().Play();
        ps.SetActive(true);
        Destroy(gameObject, 0.15f);
    }
    void WaveChange()
    {
        if (health == 3)
        {
            Enemy1.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            navP1.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            Enemy1.GetComponent<NavMeshAgent>().enabled = true;
            Enemy1.GetComponent<SimpleAIController>().enabled = true;
            currentEnemy = Enemy1;
            //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        if (health == 2)
        {
            Enemy2.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            navP2.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            Enemy2.GetComponent<NavMeshAgent>().enabled = true;
            Enemy2.GetComponent<SimpleAIController>().enabled = true;
            currentEnemy = Enemy2;
            //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        if (health == 1)
        {
            Enemy3.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            navP3.transform.position = new Vector3 (transform.position.x, yCord, transform.position.z + 3);
            Enemy3.GetComponent<NavMeshAgent>().enabled = true;
            Enemy3.GetComponent<SimpleAIController>().enabled = true;
            currentEnemy = Enemy3;
            //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void UpdateHealthText()
    {
        healthText.transform.LookAt(healthTextLook.transform);
        
        if ( distanceToPlayer > 50f )
        {
            healthText.text = "";
        }
        else if (health > 0)
            healthText.text = "Health: " + health;
        else if (health <= 0 && !healed)
        {
            //healthText.fontSize = 0.4f;
            healthText.text = "Don't you dare cure me...";
        }
        else
            healthText.text = "Curse you, Perry the-";
    }

    public IEnumerator HitAnimation()
    {
        canBeHit = false;
        anim.SetInteger("State", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("State", 0);
        canBeHit = true;
        yield return null;
    }

}
