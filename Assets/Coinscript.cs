using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinscript : MonoBehaviour
{
    public Score_handler Scr;

    public LayerMask PlayerMask;
    public float CollectRadius = 1/3f;
 

    private void Start()
    {
        Scr = FindObjectOfType<Score_handler>();
    }
    void Update()
    {
        if (Physics2D.OverlapCircle(this.transform.position, CollectRadius, PlayerMask))
        {
            Scr.FoundCoin();
            Destroy(this.gameObject);
        }
    }
}
