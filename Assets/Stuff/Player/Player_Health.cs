using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int health = 3; // player health 

    public Transform hitbox; //  Transform to set what the hitbox is
    public float hitboxRadius; // ähm yes hitbox radius...
    public LayerMask damageSource; // LayerMask to set what the damage Source is
    private bool isHit; // boolean for reacting when hit

    private bool hitCooldown = false; // cooldown for regulating the invincible frames


    public Rigidbody2D rb;
    public float knockBackForce = 10;
    public float knockBackForceUp = 2;
    public ParticleSystem HitPartikel;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;


    void Update()
    {

        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxRadius, damageSource); // reacts if the Player got hit
  
        //---------------------------------take damage---------------------------
        if (isHit && hitCooldown == false)
        {
            Instantiate(HitPartikel, transform.position, transform.rotation);
            knockBack();
            

            hitCooldown = true;
            Invoke("Cooldown", 0.6f);// is invulnerable for 0.6 seconds
            health = health - 1; // reduces health once hit
        }

        //------------------------die-----------------------
        if (health <= 0)
        {
            Destroy(gameObject); // destroy Player when health reduced to 0
        }


        //heart ui managment
        if (health >= 3) // full hearts
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (health == 2) // two hearts
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if (health == 1) // one heart
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (health <= 0) // zero hearts
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }



    }


    //---------------------------- kockback
    public void knockBack()
    {
        Transform attacker = getClosestDamageSource();
        Vector2 knockbackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public Transform getClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("WEAPON");
        float closestDistanz = Mathf.Infinity;
        Transform currentClosestDamageSource = null;


        foreach (GameObject go in DamageSources)
        {
            float currentDistanz;
            currentDistanz = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistanz < closestDistanz)
            {
                closestDistanz = currentDistanz;
                currentClosestDamageSource = go.transform;
            }
        }

        return currentClosestDamageSource;
    }


    //---------------------------cooldown-----------------
    public void Cooldown()
    {
        hitCooldown = false; // now the Player can be hit again
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitbox.position, hitboxRadius);
    }
}
