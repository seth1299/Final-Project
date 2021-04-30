using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedController : MonoBehaviour
{
    private float distanceMod = -0.05f;

    void OnCollisionEnter(Collision other)
    {
        if ( !other.gameObject.CompareTag("Terrain") )
        {
            distanceMod *= -1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + distanceMod, transform.position.y, transform.position.z);
    }
}
