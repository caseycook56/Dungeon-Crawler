using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is to help with game development
// 
public class CheatScript : MonoBehaviour
{
    private bool cheatMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("R");
            GameManagerScript.Instance.Reset();
            GameManagerScript.Instance.Level -= 1;
            GameManagerScript.Instance.nextLevel();
        }

        if (Input.GetKey(KeyCode.N))
        {
            GameManagerScript.Instance.nextLevel();
        }

        if (Input.GetKey(KeyCode.C))
        {
            cheatMode = !cheatMode;
            Debug.Log("CheatMode: "+ cheatMode);
        }
        

        
            // drink invincibility potion
            if (Input.GetKey(KeyCode.Q))
            {
            Debug.Log("1");
            GameManagerScript.Instance.Invincibility = true;
            }

            // add a heart
            else if (Input.GetKey(KeyCode.T))
            {
            Debug.Log("2");
            GameManagerScript.Instance.Hearts += 1;
            }
            
            // drink speed potion
            else if (Input.GetKey(KeyCode.Z))
            {
            Debug.Log("3");
            GameManagerScript.Instance.Speed = true;
            }

            // inscreae max heatrs
            else if (Input.GetKey(KeyCode.X))
            {
            Debug.Log("4");
            GameManagerScript.Instance.MaxHearts += 1;
            }
            // reset player postion
            else if (Input.GetKey(KeyCode.P))
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.position = new Vector3(0, 0, 0);
            }
        
    }
}
