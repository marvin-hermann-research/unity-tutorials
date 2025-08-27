using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_script : MonoBehaviour
{
    public Animator Animator;
    private bool isAttacking = false;
    private bool canAttack = true;


    public AudioClip PlayerAttack;
    public AudioSource PlayerAudioSource;

    void Update()
    {
        Animator.SetBool("attacking", isAttacking);

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack == true)
        {
            isAttacking = true;
            canAttack = false;
            Invoke("Cooldown", 0.3f);
        }
    }

    public void Cooldown()
    {
        PlayerAudioSource.clip = PlayerAttack; //choose the right clip that should play now
        PlayerAudioSource.Play();//play the sound
        canAttack = true;
        isAttacking = false;
    }
}
