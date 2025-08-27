using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleter : MonoBehaviour
{
    
    void Start()
    {
        Invoke("delete",1f);   
    }
    public void delete()
    {
        Destroy(gameObject);
    }
}
