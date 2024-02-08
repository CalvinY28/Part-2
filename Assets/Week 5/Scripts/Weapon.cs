using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float daggerSpeed = 10f;
    public float damage = 1f;
    //Rigidbody2D rb;
    //GameObject dagger;
    //Vector2 movement;

    void Start()
    {
        //Destroy(gameObject, 1f); using destory when off screen instead
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(0, daggerSpeed * Time.deltaTime, 0);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // when off screen destroy instead of using a timer
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("TakeDMG", damage, SendMessageOptions.DontRequireReceiver); // incorrect * getting error no reciever ** SendMessageOptions.DontRequireReceiver is used
        Destroy(gameObject);
    }
    //public void TakeDMG(float damage) { }


}
