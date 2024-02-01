using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed = 1;
    public List<Vector2> points;
    public float newPointThreshold = 0.2f;
    Vector2 lastPosition;
    LineRenderer lineRenderer;
    Vector2 currentPosition;
    Rigidbody2D rigidbody;
    public AnimationCurve landing;
    float landingTimer;
    SpriteRenderer spriteRenderer;
    bool isDangerZone = false;
    bool isLanding = false;
    public float score = 0;

    void OnMouseDown()
    { 
    
        points = new List<Vector2>();
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        points.Add(currentPosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

    }

    void OnMouseDrag()
    { 
    
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(currentPosition, lastPosition) > newPointThreshold) { 
            points.Add(currentPosition);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
            lastPosition = currentPosition;
        }

    
    }

    void Start() { 
        rigidbody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    
    }

    void FixedUpdate() { //fixed update for rigidbodys/physcis
        currentPosition = new Vector2(transform.position.x, transform.position.y);

        if (points.Count > 0) {
            Vector2 direction = points[0] - currentPosition;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rigidbody.rotation = -angle;
        }

        rigidbody.MovePosition(rigidbody.position + (Vector2)transform.up * speed * Time.deltaTime);

    }

    void Update() {

        if (Input.GetKey(KeyCode.Space)) {
            landingTimer += 0.1f * Time.deltaTime;
            float interpolation = landing.Evaluate(landingTimer);

            if (transform.localScale.z < 0.1f){
                Destroy(gameObject);
            }
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, interpolation);

        }

        if (points.Count > 0) {
            if (Vector2.Distance(currentPosition, points[0]) < newPointThreshold) { 
                points.RemoveAt(0);

                for(int i = 0; i < lineRenderer.positionCount - 2; i++) {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));

                }
                lineRenderer.positionCount--;

            }

        }
        if (isLanding)
        {
            landingTimer += 0.1f * Time.deltaTime;
            float interpolation = landing.Evaluate(landingTimer);

            if (transform.localScale.z < 0.1f)
            {
                Destroy(gameObject);
                IncreaseScore(10);

            }
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, interpolation);

        }
        else
        {
            if (isDangerZone)
            {
                Plane otherPlanes = FindObjectOfType<Plane>();
                if (otherPlanes != null)
                {
                    float distance = Vector3.Distance(transform.position, otherPlanes.transform.position);
                    Debug.Log("Distance " + distance);
                    if (distance < 2f) // for some reason it destroys it at the same distance even if i lower the value *** oo i see its becasue if its in dangerzone, this is very frustrating
                    {
                        Debug.Log("too close ");
                        spriteRenderer.color = Color.yellow;
                        Destroy(gameObject);
                    }
                    else
                    {
                        spriteRenderer.color = Color.white;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DangerZone"))
        {
            spriteRenderer.color = Color.red;
            isDangerZone = true;
        }

        if (collision.CompareTag("Runway"))
        {
            isLanding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DangerZone"))
        {
            spriteRenderer.color = Color.white;
            isDangerZone = false;
        }

        if (collision.CompareTag("Runway"))
        {
            isLanding = false;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void IncreaseScore(float amount) {
        score += amount;
        Debug.Log("score: " + score);
    
    }

}
