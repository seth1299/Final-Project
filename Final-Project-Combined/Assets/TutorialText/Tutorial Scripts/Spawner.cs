using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

    public GameObject monster;
    public float startTimeBtwMonster;
    private float timeBtwMonster;
    public int numberOfMonsters;

       // Update is called once per frame
    void Update()
    {
        if(timeBtwMonster <= 0 && numberOfMonsters > 0)
        {
            Instantiate(monster, transform.position, Quaternion.identity);
            timeBtwMonster = startTimeBtwMonster;
            numberOfMonsters--;
        }
        else
        {
            timeBtwMonster -= Time.deltaTime;
        }
        
    }
}
