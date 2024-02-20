using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHealth : MonoBehaviour
{
    public Slider healthSlider;

    void Start()
    {
        // Load PlayerPrefs
        float playerHealth = PlayerPrefs.GetFloat("PlayerHealth", 5f); //not working as intened
        if (healthSlider != null) {
            healthSlider.value = playerHealth;
        }
    }
}
