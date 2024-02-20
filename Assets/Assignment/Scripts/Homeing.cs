using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeing : MonoBehaviour
{
    public float homingSpeed = 5f;
    public float damage = 1f;

    private Transform playerTransform;

    void Start()
    {
        // Find the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate direction to the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * homingSpeed * Time.deltaTime);
            //RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized; // does not work as intended do not use
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) //There is bug, when the player rolls to dodge the axe just sits on them making them take dmg anyway
    {
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver); //To make the final level easier I made it so the axe collides with the arrows(maces)
            Destroy(gameObject);
    }
}
