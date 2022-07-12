using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerScript : MonoBehaviour
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
        // increase max hearts by one
        GameManagerScript.Instance.MaxHearts += 1;

        Destroy(gameObject);
    }
}
