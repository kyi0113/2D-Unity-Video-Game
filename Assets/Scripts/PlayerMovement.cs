/*
---------------------------------------------------------
 Name: Kevin Yi
 Assigment: CSC Project 
 Abstract: 
   Program collides into objects and wall jumps. 
 Due Date: 2/17/21
---------------------------------------------------------
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator ar;
    public Rigidbody2D r2d;
    private float runSpeed = 10;
    public float jumpForce = 60;
    public bool isGrounded;
    public Transform onGround;

    // locks position of sprite.
    float lockPosition = 0;

    // This is for wall jumping:
    public bool isleftWall;
    public bool isrightWall;
    public LayerMask WhatIsCollisionLayer;
    public Transform leftWall;
    public Transform rightWall;
    public float yslideSpeed; // -1f
    public float xslideSpeed; // 4f
    public float thrust = 5f;
    int jumpCounter;
    public bool wallSliding;
    public bool wallJumpingBool;
    public float xWallForce; // 5f
    public float yWallForce; // 8f
    public float wallJumpingPermission = 0;



    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        ar = GetComponent<Animator>();
    }



    void Update()
    {
        transform.rotation = Quaternion.Euler(lockPosition, lockPosition, lockPosition);
        Jump();
        wallJumping();
        ar.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 spriteMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if (!isleftWall && !isrightWall)
        {
                        
            transform.position = transform.position + spriteMovement * Time.deltaTime * runSpeed;
        } 
        if (isleftWall && Input.GetAxis("Horizontal") > 0)
        {
            transform.position = transform.position + spriteMovement * Time.deltaTime * runSpeed;
        }
        if (isrightWall && Input.GetAxis("Horizontal") < 0)
        {
            transform.position = transform.position + spriteMovement * Time.deltaTime * runSpeed;
        }    
    } 



    private void Jump()
    {
        // We need to know if the character is on the ground...
        isGrounded = Physics2D.OverlapCircle(onGround.position, .2f, WhatIsCollisionLayer);        
        // Jump condition: if jump pressed and horizontal velocity is within a certain speed..
        if (Input.GetButtonDown("Jump") && isGrounded)  
        {            
            r2d.velocity = new Vector2(r2d.velocity.x, jumpForce);
            Vector3 spriteMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            transform.position = transform.position + spriteMovement * Time.deltaTime * runSpeed;
        } else if (Input.GetButtonDown("Jump") && r2d.velocity.y == yslideSpeed)
        {
            Debug.Log("value of r2d velcoity.y == yslideSPeed ==> " + r2d.velocity.y);
            r2d.velocity = new Vector2(r2d.velocity.x, jumpForce*2 * Time.deltaTime);            
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            r2d.velocity = new Vector2(0.0f, r2d.velocity.y);
        }


    }



    private void wallJumping()
    {
        // Boolean expressions indicating if the left or right walls were indicated.
        isleftWall = Physics2D.OverlapCircle(leftWall.position, .2f, WhatIsCollisionLayer);
        isrightWall = Physics2D.OverlapCircle(rightWall.position, .2f, WhatIsCollisionLayer);


        // This is the wall sliding effect on the character.
        // If touching left wall and is NOT on ground and IS MOVING
        if (isleftWall && isGrounded == false && Input.GetAxis("Horizontal") != 0)
        {            
            wallSliding = true;
            Vector3 spriteMovement = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position = transform.position + spriteMovement;            
            r2d.velocity = new Vector2(r2d.velocity.x, Mathf.Clamp(r2d.velocity.y, -4f, float.MaxValue));
           
        }else if (isrightWall && isGrounded == false && Input.GetAxis("Horizontal") != 0)
        {
            wallSliding = true;
            r2d.velocity = new Vector2(0.0f, Mathf.Clamp(r2d.velocity.y, yslideSpeed, float.MaxValue));            
        } else
        {
            wallSliding = false;
        }


        // The actual WallJumping feature. 
        // Works ONLY IF jump key pressed, it player is sliding or starting to slide, touching a wall,
        // and has a wallJumpingPermission enabled. Meaning there can be only permission to wall jump
        // if it has previously been on the floor. If it was previously on the wall, not on the floor
        // and jumped, permission should not be granted.
        if (Input.GetButtonDown("Jump") && wallSliding == true && (isleftWall || isrightWall) && wallJumpingPermission == 1)
        {
            wallJumpingPermission = wallJumpingPermission - 1;

            wallJumpingBool = true;
       

            if (wallJumpingBool)
            {
                r2d.velocity = new Vector2(0.0f, 0.0f);
                wallJumpingBool = false;
                r2d.velocity = new Vector2(xWallForce * (-Input.GetAxis("Horizontal")), yWallForce);
            } 
        }


        // Condition to keep the wallJumping feature in check.
        if (isGrounded && !isleftWall && !isrightWall)
        {
            // now we can reset it!
            wallJumpingPermission = 1;
        }       

    }   
    



}
