using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float daggerSpeed = 5f;
    Rigidbody2D rb;
    GameObject dagger;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(0 , daggerSpeed * Time.deltaTime, 0);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}//test
