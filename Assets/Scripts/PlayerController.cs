using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Laurens additional variables
    // reference to healthManager script
    // Reference to the Panel that appears when the player gets close to a Shrine
    // reference to the platform that appears when the offering is paid
    // reference to the script that changes levels
    // reference to the cheese monk that appears in level 2
    
    private LevelChanger sceneManagerScript;
    private HealthManager healthscript;
    public GameObject ShrinePanel;
    public GameObject cheeseBridge;
    private int cheeseTax = 3;
    private bool OfferingPaid = false;
    public TextMeshProUGUI paymentText;
    public TextMeshProUGUI buttonPromptText;

    private HealthManager playerHealth;

    //Player Rigid Body
    private Rigidbody2D playerRB;

    //Player Speed Variables
    public float speed = 0f;
    public float moveX = 0f;

    //Player Jump Variables
    public float jumpStrength = 0f;
    public bool hasDoubleJump = false;
    private int totalJumps = 0;
    private bool isGrounded = true;
    
    //Cheese Variables
    public int totalCheese = 0;
    public TMP_Text cheeseIndicator;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        healthscript = this.GetComponent<HealthManager>();
        sceneManagerScript = this.GetComponent<LevelChanger>();
        cheeseIndicator.text = "testing";

        // set the shrine panel and cheese bridge to false on start
        ShrinePanel.SetActive(false);
        cheeseBridge.SetActive(false);


        hasDoubleJump = (PlayerPrefs.GetString("doubleJump", "false")== "true");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("doubleJump", hasDoubleJump.ToString());
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
        moveX = Input.GetAxisRaw("Horizontal");

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

        if(col.CompareTag("Spike"))
        {
            healthscript.editHealth(-healthscript.health);

        }

        if (col.CompareTag("Exit")){
            sceneManagerScript.loadNextLevel();

        }

        if (col.CompareTag("Monk")){
            GameObject DialogueBox = GameObject.FindGameObjectWithTag("Dialogue");
            if (DialogueBox!= null)
            {
                DialogueBox.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Shrine")){

            ShrinePanel.SetActive(true);

            if(Input.GetKeyDown(KeyCode.C)){
                payOffering();


            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if(other.CompareTag("Shrine")){
            ShrinePanel.SetActive(false);
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

    public void payOffering(){

        if(totalCheese >= cheeseTax && !OfferingPaid)
        {
            OfferingPaid = true;
            totalCheese -= (cheeseTax);
            cheeseBridge.SetActive(true);
            ShrinePanel.SetActive(false);
            paymentText.text = "This Offering is acceptable. You may proceed.";
            buttonPromptText.text = " ";

        }
    }
}
