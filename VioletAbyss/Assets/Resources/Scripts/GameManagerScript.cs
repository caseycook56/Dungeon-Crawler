using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript _instance = null;

    // player score
    private int score = 0;

    //player hearts
    private int hearts = 10;

    //current level
    private int level = 1;

    private float pixelsToUnits=100;
    
    // current row and col of the map
    private int col=30;
    private int row=20;

    private float tileSize=10;

    //max player hearts
    private int maxHearts =10;

    private bool invincibility = false;

    //how long invincibility last for
    private int invincibilityLength = 500;
    private int invincibilityCount = 0;

    private bool speed = false;

    // how speed potions last for
    private int speedLength=500;
    private int speedCount=0;

    //row and col of the portal(ladder)
    private int portalRow=0;
    private int portalCol=0;

    //edges of the map
    private float leftEdge;
    private float rightEdge;
    private float northEdge;
    private float southEdge;

    //stores if the player exist on the map
    private bool playerCreated = false;


    // the amount of prities have to be scaled 
    private float scaleSize = 1;


    public float SouthEdge
    {
        get => southEdge;
        set => southEdge = value;
    }

    public float NorthEdge
    {
        get => northEdge;
        set => northEdge = value;
    }

    public float LeftEdge
    {
        get => leftEdge;
        set => leftEdge = value;
    }

    public float RightEdge
    {
        get => rightEdge;
        set => rightEdge = value;
    }

   

    public float ScaleSize
    {
        get => scaleSize;
        set => scaleSize = value;
    }

    public int PortalRow
    {
        get => portalRow;
        set => portalRow = value;
    }

    public int PortalCol
    {
        get => portalCol;
        set => portalCol = value;
    }


    //finds the edges of the map
    public void findEdges()
    {
        rightEdge = (float)(-(0.5f * Screen.width) + (tileSize * col) + (0.5f * tileSize)) / pixelsToUnits;
        leftEdge = (float)(-(0.5f * Screen.width) + (tileSize * 0) + (0.5f * tileSize)) / pixelsToUnits;

        southEdge = (float)((Screen.height * 0.5f) - (tileSize * row) - (0.5f * tileSize)) / pixelsToUnits;
        northEdge = (float)((Screen.height * 0.5f) - (tileSize * 0) - (0.5f * tileSize)) / pixelsToUnits;
    }
   

    public bool PlayerCreated
    {
        get => playerCreated;
        set => playerCreated = value;
    }

    public bool Speed
    {
        get => speed;
        set
        {
            if (value == false)
            {
                //resets effec of speed potion
                speed = value;
                GameObject player = GameObject.FindWithTag("Player");
                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                sr.color = new Color(255, 255, 255);
            }
            else
            {
                
                speed = value;
                GameObject player = GameObject.FindWithTag("Player");

                // changes colour of player to blue when speed potion has been drunk
                player.GetComponent<SpriteRenderer>().color =  Color.blue;
                
            }

        }
    }


    public bool Invincibility
    {
        get => invincibility;
        set
        {
            if (value == false)
            {
                // resets player colour when invincibility is turned off
                invincibility = value;
                GameObject player = GameObject.FindWithTag("Player");
                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                sr.color = new Color(255, 255, 255);
            }
            else
            {
                invincibility = value;
                GameObject player = GameObject.FindWithTag("Player");
                SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

                // when invincibility potion is actived player goes green
                sr.color = Color.green;
            }
        }
    }

    public float TileSize
    {
        get => tileSize;
        set => tileSize = value;
    }

    public int MaxHearts
    {
        get => maxHearts;
        set => maxHearts = value;
    }


    public int Col
    {
        get => col;
        set => col = value;
    }

    public int Row
    {
        get => row;
        set => row = value;
    }

    public float PixelsToUnits
    {
        get => pixelsToUnits;
        set => pixelsToUnits = value;
    }


    public int Hearts
    {
        get => hearts;
        set
        {
            //sets value of heart only if it isn't higher than max
            // hearts dont change when invincbility to activated
            if (value <= maxHearts && value>0 && invincibility==false)
            {
                Debug.Log("hearts 1 ");
            
                hearts = value;
            } else if( value <= 0)
            {
                Debug.Log("game overing");
                gameOver();
            }
            
        }
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    // loads game over screen
    private void gameOver()
    {
        Debug.Log("Game Over");
        Destroy(GameObject.FindWithTag("Player"));
        playerCreated = false;
        SceneManager.LoadScene("GameOver");
    }


    public static GameManagerScript Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject GameMangerObject = new GameObject();
                GameMangerObject.name = "GameManager";
                _instance = GameMangerObject.AddComponent<GameManagerScript>();

            }
            return _instance;
        }
        
    }

    public int Score
    {
        get => score;
        set => score = value;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

        // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        // tracks how long speed potion is actve
        if (speed ==true)
        {
            speedCount++;

            // ends effect of speed potion
            if (speedCount == speedLength)
            {
                Speed = false;
                speedCount = 0;
            }
            
        }

        //tracks how long invinciblity potion is active 
        if (invincibility == true)
        {
            invincibilityCount++;

            // ends effect of invincibility potion
            if (invincibilityCount == invincibilityLength)
            {
                Invincibility = false;
                invincibilityCount = 0;
            }

        }
    }

    
    // loads next level
    public void nextLevel()
    {
        Debug.Log("next level");
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // resets values
    public void Reset()
    {
        level = 1;
        hearts = 10;
        score = 0;
        maxHearts = 10;
    }
    
}
