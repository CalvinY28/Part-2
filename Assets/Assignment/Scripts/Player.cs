using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth = 5f;
    public float moveSpeed = 5f;
    //private bool isMoving = false;
    private Vector3 targetPosition; //I should probably utilize Vector2
    public Animator animator;
    public Slider healthSlider;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentHealth = PlayerPrefs.GetFloat("PlayerHealth", maxHealth);
        UpdateHealthSlider();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Right Click to Move
        //Get Position Where Mouse Clicked
        //Move to Position

        if (currentHealth <= 0f) {
            // Player is dead dont move
            return;
        }

        if (Input.GetMouseButton(1)) {
            Vector3 targetPosition = GetMouseWorldPosition();
            MoveTo(targetPosition);
            animator.SetBool("WalkRight", true);
        }

        if (Input.GetMouseButtonUp(1)) {
            animator.SetBool("WalkRight", false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("Roll");
            DisableCollider(0.7f); //Rolling Dodges DMG
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SendMessage("TakeDamage", 1);
        }

    }

    void MoveTo(Vector3 targetPosition) {

        //Calculate Direction to Position
        //Move Player to Direction

        Vector3 direction = (targetPosition - transform.position).normalized; //Normalize to make sure Vector is same length i think (Does not work without normalizing)
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        //isMoving = true;
        float distanceToClick = Vector3.Distance(transform.position, targetPosition); //This feels more complicated then it needs to be
    }

    Vector3 GetMouseWorldPosition() {

        //Get Mouse in Screen Area
        //Convert MousePos in Screen to World

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; //Player Keeps Disappearing, This code seems to stop it from doing so. Probably something with the wonky Z axis
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthSlider();

        if (currentHealth <= 0f) {
            Die();
            animator.SetTrigger("Death");
        }
        else {
            animator.SetTrigger("TakeDMG");
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
        Invoke("RestartScene", 1f); //Give it delay so it can play animation
    }

    void RestartScene() 
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        SceneManager.LoadScene("Restart Menu");
    }

    void UpdateHealthSlider()
    {
        if (healthSlider != null) {
            healthSlider.value = currentHealth;
        }
    }

    void DisableCollider(float time)
    {
        if (boxCollider != null) {
            boxCollider.enabled = false;
            Invoke("EnableCollider", time);
        }
    }
    void EnableCollider()
    {
        if (boxCollider != null) {
            boxCollider.enabled = true;
        }
    }

}
