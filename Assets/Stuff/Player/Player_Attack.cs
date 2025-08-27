using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator Animator; // animator reference
    private bool isAttacking = false ; // bool for signaling that the player is attacking
    private bool canAttack = true; // is used to create a cooldown

    void Update()
    {

        Animator.SetBool("attacking", isAttacking); // the animation Bool "attacking" will resemble the bool "isAttacking"

        //----------------------------hiting------------------------------------------

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack == true) // when pressing the left mouse key and allowed to attack, the player attacks
        {
            isAttacking = true; // attack signal -> starts the attack animations           
            canAttack = false; //now you cannot attack until the cooldown is over
            Invoke("Cooldown",0.3f);//start timer
        }
    }
        //--------------------------hit cooldown---------------------------------------
    public void Cooldown()
    {
        canAttack = true; //whenn cooldown is over the player is able to attack again
        isAttacking = false; //attack is over
    }
}
