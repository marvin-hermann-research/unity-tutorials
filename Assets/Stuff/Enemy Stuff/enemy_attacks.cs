using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attacks : MonoBehaviour
{

    public float movementspeed = 6;

    private Transform playerPosition;

    public float SearchRadius = 10f;
    public Transform SearchRadiusPosition;
    public LayerMask PlayerMask;
    public bool playerFound;

    public bool facesLeft = true;
    public bool PlayerisLeft = true;


    public Animator Animator;
    private bool isGrounded;
    public Transform groundDetector;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    void Update()
    {

        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerFound = Physics2D.OverlapCircle(SearchRadiusPosition.position, SearchRadius, PlayerMask);


        if (playerFound)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, movementspeed*Time.deltaTime);


            if (playerPosition.position.x >= transform.position.x) 
            {
                PlayerisLeft = false;
            }
            if (playerPosition.position.x <= transform.position.x) 
            {
                PlayerisLeft = true;
            }

        }

        if (PlayerisLeft && !facesLeft)
        {
            Turn();
        }
        if (!PlayerisLeft && facesLeft)
        {
            Turn();
        }



        isGrounded = Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, whatIsGround); //uses the ground detectors position and checks if the ground is within his radius

        //animation stuff 
        Animator.SetBool("attacking", playerFound);
        Animator.SetBool("InAir", !isGrounded);

    }


    public void Turn() 
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        facesLeft = !facesLeft;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SearchRadiusPosition.position, SearchRadius);
    }

}
