using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{  
    [Tooltip("This is the game controller game object.")]
    public GameObject gc;
    public void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
        
            if (other.CompareTag("BasicEnemy"))
            {
                if (gc.GetComponent<GameController>().GetIsSwinging())
                {
                    if ( other.gameObject.name == "DogWiz" )
                    {
                        Debug.Log("Hitting dog wiz");
                        other.GetComponent<MinibossController>().GetHitBySword();
                    }
                    else
                    {
                        other.GetComponent<TargetController>().GetHitBySword();
                    }
                }
                    
            }
            else if (other.CompareTag("Dummy"))
            {
                if (gc.GetComponent<GameController>().GetIsSwinging())
                    other.GetComponent<Dummy>().GetHitBySword();
            }

        }
    }

}
