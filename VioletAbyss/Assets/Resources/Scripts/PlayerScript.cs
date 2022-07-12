using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;




public class PlayerScript : MonoBehaviour
{
    private float move = 0.02f;
    private float moveBasic = 0.02f;
    private float pixelsToUnits;

    private float leftEdge;
    private float rightEdge;
    private float xPos;
    private float xSize;

    private float yPos;
    private float ySize;

    private float northEdge;
    private float southEdge;

    private float tileSize;

    private Sprite[] sprites;



    //animation for waking animations
    private int northFirst = 61;
    private int northLast = 68;
    private int northCurrent = 61;

    private int westFirst = 69;
    private int westLast = 77;
    private int westCurrent = 69;

    private int southFirst = 78;
    private int southLast = 86;
    private int southCurrent = 78;

    private int eastFirst = 87;
    private int eastLast = 95;
    private int eastCurrent = 87;




    // Start is called before the first frame update
    void Start()
    {
        loadInfo();
    }

    private void loadInfo()
    {
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;

        leftEdge = GameManagerScript.Instance.LeftEdge;
        rightEdge = GameManagerScript.Instance.RightEdge;

        northEdge = GameManagerScript.Instance.NorthEdge;
        southEdge = GameManagerScript.Instance.SouthEdge;


        xSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        ySize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        tileSize = GameManagerScript.Instance.TileSize;

        sprites = Resources.LoadAll<Sprite>("Artwork/player1");

        move *= GameManagerScript.Instance.ScaleSize;
    }

    void Update()
    {

        //if speed potion is on then move double speed
        if (GameManagerScript.Instance.Speed)
        {
            move= moveBasic* 2* (float)System.Math.Pow((double)GameManagerScript.Instance.ScaleSize, 2.0d);
        }
        else
        {
            move =  moveBasic* (float)System.Math.Pow((double)GameManagerScript.Instance.ScaleSize, 2.0d) ;
        }


        // move to the left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            xPos = gameObject.transform.position.x;
            if (xPos> leftEdge)
            {
                //animate walking west
                if (westCurrent > westLast)
                {
                    westCurrent = westFirst;
                }
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[westCurrent];
                westCurrent++;
                gameObject.transform.position += Vector3.left * move;

            }
        }

        //move to the right
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            xPos = gameObject.transform.position.x;
            if (xPos < rightEdge)
            {
                // animate working east
                if (eastCurrent > eastLast)
                {
                    eastCurrent = eastFirst;
                }
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[eastCurrent];
                eastCurrent++;
                gameObject.transform.position += Vector3.right * move;
            }
        }

        // move down
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            yPos = gameObject.transform.position.y;
            
            if (yPos > southEdge )
            {
                // animate working down
                if (southCurrent > southLast)
                {
                    southCurrent = southFirst;
                }
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[southCurrent];
                southCurrent++;
                gameObject.transform.position += Vector3.down * move;
            }

        }

        //move up
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            yPos = gameObject.transform.position.y;
            if (yPos< northEdge)
            {
                //animate walking up
                if (northCurrent > northLast)
                {
                    northCurrent = northFirst;
                }

                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[northCurrent];
                northCurrent++;
                gameObject.transform.position += Vector3.up * move;
            }
        } 
    }
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}