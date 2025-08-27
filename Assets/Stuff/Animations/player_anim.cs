using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_anim : MonoBehaviour
{
    public int movementSpeed = 10;
    public int jumpingPower = 15;

    public bool rotated = false;
    public bool isGrounded;
    public Rigidbody2D rb;



    public Animator Animator; //animator reference
    private bool isJumping; 
    private bool isWalking;


    public Transform groundDetector;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.3f;



    public AudioClip PlayerJump;
    public AudioSource PlayerAudioSource;

    void Update()
    {
        //-----------------------------------------------------------------movement
        if (Input.GetKey("d")) // input for walking right
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime); //move players transform in the right direction by the ammount of movement speed
            isWalking = true; //set walking anim variable true ->enable walking anim
        }
        if (Input.GetKey("a")) // input for walking left
        {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime); //move players transform in the left direction by the ammount of movement speed
            isWalking = true; //set walking anim variable true ->enable walking anim
        }

        if(Input.GetKeyUp("d") || Input.GetKeyUp("a")) // if you stop walking
        {
            isWalking = false; //set walking anim variable false ->disable walking anim
        }

        Animator.SetBool("walking", isWalking); //the anim bool "walking" mirrors the bool "isWalking"

        //------------------------------------------------------------------turning    
        if (Input.GetKey("d") && !Input.GetKey("a") && rotated == true || Input.GetKey("a") && !Input.GetKey("d") && rotated == false) //if you only press in one direction and you have not rotated yet -> Input.GetKey("a") && !Input.GetKey("d") fixes issue of spasam when pressing both
        {
            rotated = !rotated; //change the state of rotated to signal that the player has been now rotated
            Vector3 Scaler = transform.localScale; // get scale of the player
            Scaler.x *= -1; // inverts the scale wich means it flips the modell
            transform.localScale = Scaler; //apply rotation
        }


        //-------------------------------------------------------------------Jumping
        if (Input.GetKeyDown("space") && isGrounded) // input for jumping not else if because you should also be able to jump whilst walking
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);// applies short force impulse up
            PlayerAudioSource.clip = PlayerJump; //choose the right clip that should play now
            PlayerAudioSource.Play();//play the sound
        }

        if (isGrounded)
        {
            isJumping = false;
        }
        if (!isGrounded)
        {
            isJumping = true;
        }

        Animator.SetBool("jumping", isJumping);

        //-------------------------------------------------------------------Ground detection
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, whatIsGround); //uses the ground detectors position and checks if the ground is within his radius
        //when unity 3D Phsyics.OverlapSphere
    }
}
