// Written by Fisher Hensley | 4/18/2021 | UCF DIG-4715 Project 3 | 9 Lives Studio
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIController : MonoBehaviour
{
    public float lookDist;
    public Transform[] navPoints;
    private int destPoint = 0;
    private bool pursuing, goingToNextPoint = false;
    private GameObject player;
    private NavMeshAgent navAgent;

    /* These links are to webpages which the AI was based on, incase I am not here to help.
       NavAgent: https://docs.unity3d.com/Manual/nav-AgentPatrol.html
       Raycast: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html */
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        pursuing = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GotoNextPoint();
    }
    void Update()
    {
        // This causes the enemy to move to the next point in the array using the called function.
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f && pursuing == false && !goingToNextPoint)
        {
            StartCoroutine("GoToNextPoint");
        }

        // This is used for debugging the raycast.
        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.right * -1 * lookDist, Color.red, 0.5f, false);

        // This is the actual raycast that the enemy uses to find the player.
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.right * -1, lookDist, LayerMask.GetMask("NPC and PC")))
        {
            pursuing = true;
        }

        // This causes the enemy to pursue the player.
        if (pursuing == true) 
        {
            navAgent.destination = player.transform.position;
        }
    }

    public IEnumerator GoToNextPoint()
    {
        goingToNextPoint = true;
        yield return new WaitForSeconds(2f);

        if (navPoints.Length == 0)
        {
            StopCoroutine("GoToNextPoint");
            goingToNextPoint = false;
            yield return null;
        }
        navAgent.destination = navPoints[destPoint].position;
        destPoint = (destPoint + 1) % navPoints.Length;
        goingToNextPoint = false;

        yield return null;
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
}
