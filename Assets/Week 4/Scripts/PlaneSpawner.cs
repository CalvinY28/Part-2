using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject[] planeSprite; // array store things
    public GameObject plane;
    //public Transform spawn;
    float timer;
    float timerTarget = 5;

    void Start()
    {
        
    }


    void Update() {
        timer += Time.deltaTime;
        if (timer > timerTarget)
        {
            float randomPosX = Random.Range(-5, 5);
            float randomPosY = Random.Range(-5, 5);
            Quaternion randomRotationZ = Quaternion.Euler(0, 0, Random.Range(0, 360)); // Quaternion is basically just rotation
            Instantiate(plane, new Vector3(randomPosX, randomPosY, 0), randomRotationZ); //Instantiate plane with random pos and random rotation ** remember to add NEW in Vector 3 when making smth

            Plane planeSpeed = plane.GetComponent<Plane>(); // note naming got confusing
            float randomSpeed = Random.Range(1, 3);
            planeSpeed.speed = randomSpeed;
            
            timer = Random.Range(0, 4); // new prefab randomly between 1 and 5
        }
    }
    GameObject GetRandomSprite() { 
        int randomIndex = Random.Range(0, planeSprite.Length); // pull random sprite from array ** i think i need to instantiate this and change some things
        return planeSprite[randomIndex];
    }

}
