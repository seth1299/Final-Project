/* Written by Fisher Hensley | 4/16/2021 | UCF DIG-4715 Project 3 | 9 Lives Studio
   Some parts of this code were taken from Seth Grime's TargetController. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AIGuardController : MonoBehaviour
{
    public int health;
    public float lookDist;
    public Transform[] navPoints;
    public GameObject gameController;
    public GameObject ps;
    public TextMeshProUGUI healthText;
    private int destPoint = 0;
    private bool pursuing;
    private bool healed;
    private Vector3 currentLocation;
    private GameObject player;
    private NavMeshAgent navAgent;
    
    /* These links are to webpages which the AI was based on, incase I am not here to help.
       NavAgent: https://docs.unity3d.com/Manual/nav-AgentPatrol.html
       Raycast: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html */
    
    void Awake()
    {
        // ps.SetActive(false); // Turn this on in updated game.
    }
    void Start()
    {
        healed = false;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        pursuing = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GotoNextPoint();
    }

    void Update()
    {
        UpdateHealthText();
        Debug.Log ("Pursuit: " + pursuing);
        // This causes the enemy to move to the next point in the array using the called function.
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f && pursuing == false)
        {
            GotoNextPoint();
        }
        // This projects a raycast so that the enemy can detect the player.
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, lookDist, LayerMask.GetMask("NPC and PC")))
        {
            Debug.Log("Raycast Hit");
            pursuing = true;
        }
        // This causes the enemy to pursue the player.
        if (pursuing == true) 
        {
            navAgent.destination = player.transform.position;
        }
    }

    void GotoNextPoint()
    {
        if (navPoints.Length == 0)
        {
            return;
        }
        navAgent.destination = navPoints[destPoint].position;
        destPoint = (destPoint + 1) % navPoints.Length;
    }

    void OnCollisionEnter(Collision hit)
    {
        // The code from here and on is mostly if not all from Seth's TargetController.
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
            }
        }
        else if (hit.gameObject.tag == "Sword")
        {
            if (health > 0)
            {
                StartCoroutine("GetHitBySwordForReal");
            }
        }
    }

    public IEnumerator GetHitByArrow()
    {
        health--;
        UpdateHealthText();
        yield return null;
    }

    public IEnumerator GetHitBySwordForReal()
    {
        health--;
        UpdateHealthText();
        for (int i = 0; i < 60; i++)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.125f);
            yield return null; // Not sure why there are three "yield return null"'s here, ask Seth.
            yield return null;
        }
        yield return null;
    }

    private void UpdateHealthText()
    {
        if (health > 0)
            healthText.text = "Health: " + health;
        else if (health <= 0 && !healed)
        {
            healthText.text = "Now's your chance, heal them with your magical ability!";
        }
        else
            healthText.text = "This enemy is now cured and is saved!";
    }

    public void GetHealed()
    {
        healed = true;
        // GetComponent<AudioSource>.Play(); // Uncomment lines 134-136 when in updated game.
        // ps.SetActive(true);
        // Destroy(gameObject, 0.15f);
    }

    public void GetHitBySword()
    {
        if (health > 0)
            StartCoroutine("GetHitBySwordForReal");
    }
}
