using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinibossController : MonoBehaviour
{
    public float yCord;
    public GameObject Enemy1, Enemy2, Enemy3, navP1, navP2, navP3, ps;
    private int health;
    private bool healed;
    private GameObject currentEnemy, protectiveShield;

    void Start()
    {
        protectiveShield = GameObject.Find("Protective Shield");
        protectiveShield.SetActive(false);
        health = 4;
        healed = false;
    }
    void Update()
    {
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
        Debug.Log("Hit by something");
        if (hit.gameObject.tag == "Projectile")
        {
            if (hit.gameObject.GetComponent<ProjectileController>().type == false && health > 0)
            {
                Destroy (hit.gameObject);
                health--;
                WaveChange();
            }
            else if (hit.gameObject.tag == "Projectile" && hit.gameObject.GetComponent<ProjectileController>().type == true && health <= 0)
            {
                Destroy (hit.gameObject);
                StartCoroutine("GetHealed");
            }
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
}
