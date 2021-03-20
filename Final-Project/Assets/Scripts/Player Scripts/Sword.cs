using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{  

    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 targetPosition = new Vector3(-0.453f, 0.82f, 1.014f);
        //StartCoroutine("SwingSword");
    }

    // Update is called once per frame
    void Update()
    {

        //currentPosition = gameObject.transform.position;

        //currentPosition = currentPosition.RotateTowards(currentPosition, targetPosition);
    }

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

    public IEnumerator SwingSword()
    {
        yield return null;
        // Code goes here
    }
}
