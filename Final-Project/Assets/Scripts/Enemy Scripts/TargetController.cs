using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Projectile")
        {
            if (hit.gameObject.GetComponent<ProjectileController>().type == false)
            {
                Debug.Log("Ouchies!");
                Destroy (hit.gameObject);
                // Code for the enemy getting hit would go here.
            }
            else
            {
                Debug.Log("Poof!");
                Destroy (hit.gameObject);
                // Code for the enemy being pacified would go here.
            }
        }
    }
}
