using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPotionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManagerScript.Instance.Invincibility = true;

        Destroy(gameObject);
    }
}
