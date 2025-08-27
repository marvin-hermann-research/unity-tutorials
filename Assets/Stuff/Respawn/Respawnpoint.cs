using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnpoint : MonoBehaviour
{
    public Respawn_handler Rhandler;

    public Transform playerDetector;
    public LayerMask PlayerMask;
    public float detectRadius = 1;

    public GameObject Flag;
  
    void Update()
    {
        if (Physics2D.OverlapCircle(playerDetector.position, detectRadius, PlayerMask)) //if player touches flagg
        {
            Rhandler.SetRespawnPoint(this.transform.position, this.gameObject); // send position and own game Object to the handler
            Flag.SetActive(true);
        }
    }
}
