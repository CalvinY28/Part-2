using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 destination;
    Vector2 movement;
    public float speed = 3f;
    bool clickingOnSelf = false;
    bool isDead = false;
    public float health;
    public float maxHealth = 5;


    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = false;
    }

    
    void Update()
    {
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && !clickingOnSelf)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        animator.SetFloat("Movement", movement.magnitude);

    }

    private void FixedUpdate()
    {
        if(isDead) return; // use return to stop doing the function
        movement = destination - (Vector2)transform.position;
        if (movement.magnitude < 0.1f) {
            movement = Vector2.zero;
        }
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        if (isDead) return;
        clickingOnSelf = true;
        TakeDMG(0.5f);
    }
    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }

    void TakeDMG(float damage) 
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0) {
            isDead = true;
            animator.SetTrigger("Death");
        }
        else
        {
            isDead = false;
            animator.SetTrigger("TakeDMG");
        }
    }
}
