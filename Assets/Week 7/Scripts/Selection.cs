using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Color selectedColour;
    public Color unSelectedColour;
    public float speed = 10f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Selected(false);
    }

    private void OnMouseDown()
    {
        Controller.SetSelectedPlayer(this);
    }

    public void Selected(bool isSelected) { 
        if (isSelected)
        {
            spriteRenderer.color = selectedColour;
        }
        else
        {
            spriteRenderer.color = unSelectedColour;
        }
    }

    public void Move(Vector2 direction) { 
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

}
