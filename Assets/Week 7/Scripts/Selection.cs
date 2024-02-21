using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color selectedColour;
    public Color unSelectedColour;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Selected(false);
    }

    private void OnMouseDown()
    {
        Selected(true);
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

}
