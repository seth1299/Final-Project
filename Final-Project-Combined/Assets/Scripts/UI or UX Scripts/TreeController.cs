using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public GameObject particleSystem;
    void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                if (other.gameObject.GetComponent<ProjectileController>().type == true)
                {
                    Destroy(other.gameObject);
                    particleSystem.SetActive(true);
                    Destroy(gameObject, 2.25f);
                }
            }
        }
    }
}
