using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_handler : MonoBehaviour
{
    public Vector3 CurrentRespawnCords;
    public GameObject CurrentRespawnPoint;

    public GameObject Player;

    private void Start()
    {
        CurrentRespawnCords = Player.transform.position; // set first respawn point to where te pplayer starts the level
    }

    public void SetRespawnPoint(Vector3 cords, GameObject checkpoint) //set respawn point and get cords for point
    {
        CurrentRespawnCords = cords;
        CurrentRespawnPoint = checkpoint;
    }

}
