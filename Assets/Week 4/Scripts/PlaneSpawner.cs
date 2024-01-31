using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject plane;
    public Transform spawn;
    float timer;
    float timerTarget = 5;
    Vector3 spawner;

    void Start()
    {
        
    }


    void Update() {
        timer += Time.deltaTime;
        if (timer > timerTarget)
        {
            
            Instantiate(plane, spawn.position, spawn.rotation);

            
            timer = Random.Range(0, 4); // new prefab randomly between 1 and 5
        }
    }
}
