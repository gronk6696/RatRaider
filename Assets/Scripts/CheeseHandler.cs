using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheeseHandler : MonoBehaviour
{
    
    public PlayerController player;
    public string clipName;
    public AudioManager AM;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AM.playClip(clipName);
            player.EditCheese(1);
            
            Destroy(this.gameObject);
        }
    }
}
