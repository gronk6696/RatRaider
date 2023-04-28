using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    //Player Rigid Body
    private Rigidbody2D playerRB;

    //Player Speed Variables
    public float speed = 0f;

    //Player Jump Variables
    public float jumpStrength = 0f;
    private bool hasDoubleJump = true;
    private int totalJumps = 0;
    private bool isGrounded = true;
    
    //Cheese Variables
    public int totalCheese = 0;
    public TMP_Text cheeseIndicator;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        cheeseIndicator.text = "testing";
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        JumpHandler();
        cheeseIndicator.text = totalCheese.ToString();
    }
    
    // Player AD Movement
    private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector3 scale = transform.localScale;

        //Player Direction
        if (moveX > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        if (moveX < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        if (Mathf.Abs(moveX) > 0)
        {
            transform.localScale = scale;
        }

        transform.position += new Vector3(moveX, 0f, 0f) * (speed) * Time.deltaTime;
    }

    // Ground Checks
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
            totalJumps = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Jump Handler
    private void JumpHandler()
    {
        if (Input.GetButtonDown("Jump") && ((isGrounded) || (hasDoubleJump && totalJumps < 2 && totalJumps >= 1)))
        {
            //isGrounded = false;
            totalJumps++;
            playerRB.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
        }
    }
    
    // Cheese Modifier
    public void EditCheese(int amount)
    {
        totalCheese += amount;
    }

}
