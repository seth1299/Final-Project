using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float force;
    [Tooltip("False is used for Arrows. True is used for magic.")]
    public bool type;
    Rigidbody projectileBody;

    // This is the target position for where the arrow will be shot.
    private Vector3 targetPosition;

    //private Camera cam;
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
            if ( Camera.main.transform.rotation.x < 0)
                projectileBody.velocity = player.transform.forward * force + Vector3.up * (Mathf.Abs(Camera.main.transform.rotation.x) * (force * 4.5f));
            else
                projectileBody.velocity = player.transform.forward * force + Vector3.up * (Mathf.Abs(Camera.main.transform.rotation.x) );
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

    void OnCollisionEnter(Collision other)
    {
        if ( other != null )
        {
            if (other.gameObject.tag == "Solid" || other.gameObject.tag == "Terrain")
            {
                projectileBody.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}