using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int health = 0;
    public int maxHealth = 0;

    public GameObject deathScreen;
    public TMP_Text healthIndicator;

    private PlayerController controls;

    private void Start()
    {
        controls = GetComponent<PlayerController>();
        healthIndicator.text = "test";
    }

    public void editHealth(int amount)
    {
        health += amount;
    }

    public void minusHealth(int amount){
        health -= amount;

    }
    
    void Update()
    {
        DeathCheck();
        healthIndicator.text = health.ToString();
    }

    public void DeathCheck()
    {
        if (health <= 0)
        {
            Debug.Log("death");
            Destroy(controls);
            deathScreen.SetActive(true);
        }
    }
    
    
    
}
