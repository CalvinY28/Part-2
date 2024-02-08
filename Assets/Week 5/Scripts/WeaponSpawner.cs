using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject Dagger;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { 
            Instantiate(Dagger, transform.position, Quaternion.identity);
        }
    }
}
