using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheeseWheelEnemy : MonoBehaviour
{
    public float movementRadius;
    public float movementSpeed;
    private Vector3 currentPos;
    private Vector3 startingPos;
    private bool direction = false;

    private float damageCooldown = 0.3f;
    private float timer = 0f;

    public GameObject player;
    private HealthManager playerHM;
    private Rigidbody2D playerRB;
    private PlayerController playerC;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        startingPos = transform.position;
        playerHM = player.GetComponent<HealthManager>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerC = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        WheelMovement();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    void WheelMovement()
    {
        currentPos = transform.position;
        Vector3 scale = transform.localScale;
        int moveX = 0;
        if (Mathf.Abs(currentPos.x - startingPos.x) >= movementRadius)
        {
            direction = !direction;
        }
        
        if (direction)
        {
            scale.x = Mathf.Abs(scale.x);
            moveX = 1;
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
            moveX = -1;
        }
        
        transform.localScale = new Vector3(moveX,1,1);
        transform.position += new Vector3(moveX, 0, 0) * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && timer <=0)
        {
            playerHM.editHealth(-1);
            playerRB.AddForce(Vector2.up + new Vector2((playerC.moveX)*-1, 0) * new Vector2(playerC.jumpStrength,playerC.jumpStrength), ForceMode2D.Impulse);
            timer = damageCooldown;
        }
    }
}
