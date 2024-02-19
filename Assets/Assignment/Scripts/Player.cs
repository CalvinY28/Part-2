using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Right Click to Move
        //Get Position Where Mouse Clicked
        //Move to Position

        if (Input.GetMouseButton(1)) {
            Vector3 targetPosition = GetMouseWorldPosition();
            MoveTo(targetPosition);
        }

        if (isMoving) { // I wanted to make it so the player moves until the targetPosition is reached but something is wrong. It is fine, movement works without rigid
            float distanceToClick = Vector3.Distance(transform.position, targetPosition);
            if (distanceToClick > 0.1f) {
                isMoving = false;
            }
        }
    }

    void MoveTo(Vector3 targetPosition) { 

        //Calculate Direction to Position
        //Move Player to Direction

        Vector3 direction = (targetPosition - transform.position).normalized; //Normalize to make sure Vector is same length i think (Does not work without normalizing)
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        isMoving = true;
    }

    Vector3 GetMouseWorldPosition() { 

        //Get Mouse in Screen Area
        //Convert MousePos in Screen to World

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; //Player Keeps Disappearing, This code seems to stop it from doing so. Probably something with the wonky Z axis
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

}
