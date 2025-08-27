using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{

    public int health = 100; // enemy health 

    public Transform hitbox; //  Transform to set what the hitbox is
    public float hitboxRadius = 1.45f; // ähm yes hitbox radius...
    public LayerMask damageSource; // LayerMask to set what the damage Source is
    private bool isHit; // boolean for reacting when hit

    private bool hitCooldown =false; // cooldown for regulating the invincible frames

    public Rigidbody2D rb;
    public float knockBackForce = 10;
    public float knockBackForceUp = 2;

    public ParticleSystem HitPartikel;
    void Update()
    {  
        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxRadius, damageSource); // reacts if the Enemy got hit

        //---------------------------------take damage---------------------------
        if (isHit && hitCooldown==false)
        {
            Instantiate(HitPartikel, transform.position, transform.rotation); // spawne das partikelsystem an der stelle des getroffenen

            Knockback(); //call the impactful things



            hitCooldown = true;
            Invoke("Cooldown",0.8f);// is invulnerable for 0.8 seconds
            health = health - 25; // reduces health once hit
        }
        //------------------------die-----------------------
        if (health <= 0)
        {
            Invoke("delete", 0.3f);    // destroy enemy when health reduced to 0
        }
    }

    //--------------------------cooldown------------------------
    public void Cooldown()
    {
        hitCooldown = false; // now the enemy can be hit again
    }

    public void delete()
    {
        Destroy(gameObject); // delete particle system to not clutter the scene
    }

    public void Knockback()
    {
        //Invoken bei tod einbauen so dass er noch etwas filegt und dann stirbt
        // Linear drag hoch schrauben wenn er zu weit rutsch beim rb

        Transform attacker = getClosestDamageSource(); // finde position des nähsten angreifers
        
        Vector2 knockBackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0); // rückstoß richtung herleiten eigene position - gegner position -> = entgegengesetzte richtung des gegners
        rb.velocity = new Vector2(knockBackDirection.x , knockBackForceUp) * knockBackForce; // auf den rb eine kraft in die rückstoßrichtung, so stark nach oben wie knockbackforce up und generell alles mit knockbackforce intensiviert
    }

    public Transform getClosestDamageSource() //nächsten gegner finden -> diese Funktion ist vom Typen Transform und gibt als rückgabewert das nähste Objekt welches schaden anrichten kann
    {
        GameObject[] SchadensQuellen = GameObject.FindGameObjectsWithTag("WEAPON"); // array füllen mit allen Objekten der Szene die alls schadensquelle zählen können
        float naehsteDistanz = Mathf.Infinity; // distanz zum nähsten gegner ins unendliche setzen
        Transform aktuellerNähsteSchadensquelle = null; // reseten den nähsten gegner
   

        foreach (GameObject go in SchadensQuellen) // überprüfen für jeden eintrag des arrays folgendes
        {
            float aktuelleDistanz;
            aktuelleDistanz = Vector3.Distance(transform.position, go.transform.position); // distanz zwischen eigenem Objekt und dem gewählten Objekt des arrays herausfinden
            if (aktuelleDistanz < naehsteDistanz) // dann wenn diese neue distanz kleiner ist als die distanz des vorherigen durchlaufs -> vom vorherigen gewählten array eintrag
            {
                naehsteDistanz = aktuelleDistanz; // diese neue distanz als nähste distanz wählen
                aktuellerNähsteSchadensquelle = go.transform; // und gegner wird als nähster gewählt
            }
        }
        return aktuellerNähsteSchadensquelle; // gebe den nähsten angreifer zurück
    }

}
