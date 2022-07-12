using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hazard");
        if (collision.CompareTag("Player"))
        {
            // does one point of dmamage to the player
            GameManagerScript.Instance.Hearts -= 1;
        }
    }

 
}
