using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Rigid Body
    private Rigidbody2D playerRB;

    //Player Speed Variables
    public float speed = 0f;

    //Player Jump Variables
    public float jumpStrength = 0f;
    private bool hasDoubleJump = false;
    private int totalJumps = 0;
    private bool isGrounded = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        JumpHandler();
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
        }else if (moveX < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

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
}
