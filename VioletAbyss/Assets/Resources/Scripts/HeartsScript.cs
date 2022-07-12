using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsScript : MonoBehaviour
{
   
    private void Start()
    {
        
        
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManagerScript.Instance.Hearts +=1;
       
        Destroy(gameObject);
    }
}
