using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 10f;
    public float damage = 1f;
    Rigidbody2D rb;

    public float minScale = 200f; 
    public float maxScale = 500f; 
    public float maxDistance = 10f;
    private Vector2 initialPosition;

    public AnimationCurve curve;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = transform.up * arrowSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        //Trying to make projectile smaller the farther it traveled
        float distanceTraveled = Vector2.Distance(initialPosition, transform.position); // this is confusing study lerp
        // Calculate the scale from distance traveled
        float t = Mathf.Clamp01(distanceTraveled / maxDistance); //01 takes 1 arguement
        float currentScale = Mathf.Lerp(minScale, maxScale, t);
        // Apply scale to arrow
        transform.localScale = new Vector3(currentScale, currentScale, 1f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            collision.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
