using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject weaponPrefab;
    public float spawnInterval = 2f;

    private void Start()
    {
        //I dont know if im allowed to use Invoke as a timer but this seemed easiest looking through unity's code
        InvokeRepeating("SpawnWeapon", 0f, spawnInterval);
    }

    void SpawnWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
    }
}
