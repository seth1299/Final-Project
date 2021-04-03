using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{  
    // This checks to see if the other thing the sword is colliding with is an enemy, then if so it calls the "GetHitBySword()" function on the enemy's controller script
    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
        
            if (other.CompareTag("BasicEnemy"))
            {
                other.GetComponent<TargetController>().GetHitBySword();
            }

        }
    }

}
