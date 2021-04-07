using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    public float lookDist;
    public Transform[] navPoints;
    private int destPoint = 0;
    private float playerDist;
    private bool pursuing;
    private bool guardStunned;
    private bool playerHiding;
    private Vector3 currentLocation;
    private GameObject player;
    private NavMeshAgent navAgent;

    // https://docs.unity3d.com/Manual/nav-AgentPatrol.html
    // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        pursuing = false;
        playerHiding = true;
        player = GameObject.FindGameObjectWithTag("Player");
        GotoNextPoint();
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
    void Update()
    {
        Debug.Log("Pursuit: " + pursuing);
        // playerHiding = player.GetComponent<PlayerController>().hiding;
        playerDist = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (playerDist > 100)
        {
            playerHiding = true;
        }
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f && pursuing == false)
        {
            GotoNextPoint();
        }
        else if (pursuing == true)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            navAgent.destination = player.transform.position;
            // playerHiding = player.GetComponent<PlayerController>().hiding;
        }
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, lookDist, LayerMask.GetMask("NPC and PC")))
        {
            Debug.Log("Raycast Hit");
            playerHiding = false;
            pursuing = true;
        }
        if (playerHiding == true)
        {
            Debug.Log("Player is Hiding");
            pursuing = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (guardStunned == true)
        {
            gameObject.transform.position = currentLocation;
        }
    }
    void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Projectile")
        {
            currentLocation = gameObject.transform.position;
            StartCoroutine("Stunned");
            Destroy(Hit.gameObject);
        }
        if (Hit.gameObject.tag == "Player")
        {
            pursuing = false;
            StartCoroutine("HitReset");
        }
    }
    IEnumerator Stunned()
    {
        guardStunned = true;
        yield return new WaitForSecondsRealtime(5);
        guardStunned = false;
        pursuing = false;
        navAgent.ResetPath();
        GotoNextPoint();
    }
    IEnumerator HitReset()
    {
        Debug.Log("Hit is being reset");
        guardStunned = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSecondsRealtime(1);
        pursuing = false;
        navAgent.ResetPath();
        guardStunned = false;
        GotoNextPoint();
    }
}
