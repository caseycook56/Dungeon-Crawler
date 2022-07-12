using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setUpSceneScript : MonoBehaviour
{
    private float pixelsToUnits;
    private GameObject player;

    private int numCol;
    private int numRow;
    private float tileSize;
    private GameObject hazard;
    private GameObject floor;
    private GameObject portal;




    void Awake()
    {
        // creates player only when one doesn't exist
        if (GameManagerScript.Instance.PlayerCreated ==false)
        {
            player = Instantiate((GameObject)Resources.Load("Prefabs/Player"));
            GameManagerScript.Instance.PlayerCreated = true;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
        
        
        // sets pxiel to units
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits = player.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        // sets camera
        Camera.main.orthographicSize = (Screen.height * 0.5f) / pixelsToUnits;
        
        // gets tile size
        portal = (GameObject)Resources.Load("Prefabs/Ladder");
        GameManagerScript.Instance.TileSize = portal.GetComponent<SpriteRenderer>().sprite.bounds.size.x * pixelsToUnits * 2;
        
    }


    private void Start()
    {

    }




}