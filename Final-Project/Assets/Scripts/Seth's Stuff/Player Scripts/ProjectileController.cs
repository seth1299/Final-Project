using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float force;
    [Tooltip("False is used for Arrows. True is used for magic.")]
    public bool type;
    Rigidbody projectileBody;
    void Awake()
    {
        projectileBody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine("DestroySelf");
    }

    void Update()
    {
        if(transform.position.magnitude > 500.0f)
        {
            Destroy(gameObject);
        }
    }
    public void Launch(GameObject player)
    {
        if (type == false)
        {
            projectileBody.velocity = player.transform.forward * force + Vector3.up;
        }
        else
        {
            projectileBody.velocity = player.transform.forward * force;
        }
    }

    // This destroys the projectile after 2 seconds so that the projectiles don't just continuously move forward forever.
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}