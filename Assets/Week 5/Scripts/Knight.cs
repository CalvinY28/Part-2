using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public HealthBar healthbar;

    public Slider startHealth;
    public Slider savedHealth;


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
        if (Input.GetMouseButtonDown(0) && !clickingOnSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        animator.SetFloat("Movement", movement.magnitude);

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
        }

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
        gameObject.SendMessage("TakeDMG", 1);
        //TakeDMG(1f);
        //healthbar.TakeDMG(1);
    }
    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }

    public void TakeDMG(float damage) 
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
