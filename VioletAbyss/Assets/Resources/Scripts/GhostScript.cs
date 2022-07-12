using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour


{
    // indexes for the sprite array
    private int currSpriteIndex = 0;
    private int maxSpriteIndex = 3;

    // decides when to animate sprite
    private int animateLoopCount = 0;
    private int animateLoopMax = 15;

    // edges of the map
    private float leftEdge;
    private float rightEdge;
    private float northEdge;
    private float southEdge;

    //postion of  enemy
    private float xPos;
    private float xSize;
    private float yPos;
    private float ySize;


    private float pixelsToUnits;
    private float tileSize;

    // how much sprite moves each time
    private float move = 0.1f;

    // when the ghost changes direction
    private int changeDirection = 800;
    private int countChangeDirection = 0;

    private bool flipRight = false;

    // when the ghost movements
    private int moveEnemyCount =0;
    private int moveEnemy = 250;
  

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    //current direction enemy is moving in
    private Direction enemyDirection = Direction.Right;

    //list of sprites for animation
    private List<Sprite> sprites = new List<Sprite>();



    // Start is called before the first frame update
    void Start()
    {
        loadinfo();
    }


    // load map info
    private void loadinfo()
    {
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;
        leftEdge = GameManagerScript.Instance.LeftEdge;
        rightEdge = GameManagerScript.Instance.RightEdge;

        northEdge = GameManagerScript.Instance.NorthEdge;
        southEdge = GameManagerScript.Instance.SouthEdge;

        xSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        ySize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;


        tileSize = GameManagerScript.Instance.TileSize;

        move *= GameManagerScript.Instance.ScaleSize;
        loadSprites();
        changeEnemyDirection();
    }


    //load spritesd for animation
    private void loadSprites()
    {
        sprites.Add(Resources.Load<Sprite>("Artwork/ghost-halo-1"));
        sprites.Add(Resources.Load<Sprite>("Artwork/ghost-halo-2"));
        sprites.Add(Resources.Load<Sprite>("Artwork/ghost-halo-3"));
        sprites.Add(Resources.Load<Sprite>("Artwork/ghost-halo-4"));
    }


    // changes the ghost direction
    //moves in a random direction
    private void changeEnemyDirection()
    {
        int random = Random.Range(0, 4);

        // move down
        if (random == 0)
        {
            enemyDirection = Direction.Down;
        }

        // move up
        else if (random == 1)
        {
            enemyDirection = Direction.Up;
        }

        //move left
        else if (random == 2)
        {
            enemyDirection = Direction.Left;
            flipAnimation(!flipRight);
           
        }

        //move right
        else if (random == 3)
        {
            enemyDirection = Direction.Right;
            flipAnimation(flipRight);
        }
    }

    // decides if the sprite needs to be flipped for animation
    private void flipAnimation(bool flip)
    {
        if (flip)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }



    // Update is called once per frame
    void Update()

    {
        // random so that all ghosts down move at the same time
        int numRandom = Random.Range(0, 10);

        if (numRandom < 5)
        {
            countChangeDirection++;
            moveEnemyCount++;

            //decides when to move
            if (moveEnemyCount == moveEnemy)
            {
                moveDirection();
                moveEnemyCount = 0;
            }


            //decides when to change direction
            if (countChangeDirection == changeDirection)
            {
                changeEnemyDirection();
                countChangeDirection = 0;
            }
            animateEnemy();
        }
    }

    // move the ghost in the current direction
    private void moveDirection()
    {
        //fine current ghost position
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;

        // move left
        if (enemyDirection== Direction.Left)
        {
            if (xPos - xSize > leftEdge)
            {
                gameObject.transform.position += Vector3.left * move;

            }
            else
            {
                changeEnemyDirection();
            }

        }

        // move left
        else if(enemyDirection == Direction.Right)
        {
            if (xPos + xSize < rightEdge)
            {
                gameObject.transform.position += Vector3.right * move;
            }
            else
            {
                changeEnemyDirection();
            }

        }

        //move up
        else if(enemyDirection == Direction.Up)
        {
            if (yPos + ySize < northEdge)
            {
                gameObject.transform.position += Vector3.up * move;
            }
            else
            {
                changeEnemyDirection();
            }
        }

        //move down
        else if(enemyDirection == Direction.Down)
        {
            if (yPos + ySize > southEdge)
            {
                gameObject.transform.position += Vector3.down * move;
            }
            else
            {
                changeEnemyDirection();
            }
        }
    }

    //animate ghsot
    private void animateEnemy()
    {
        if (animateLoopCount == animateLoopMax)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currSpriteIndex];
            if (currSpriteIndex < maxSpriteIndex)
            {
                currSpriteIndex++;
            }
            else
            {
                currSpriteIndex = 0;
            }
               
            animateLoopCount = 0;
        }
        else
        {
            animateLoopCount++;
        }
            
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with player, the player loses 1 heart
        Debug.Log("ghost");
        if (collision.CompareTag("Player"))
        {
            GameManagerScript.Instance.Hearts -= 1;
        }
    }
}
