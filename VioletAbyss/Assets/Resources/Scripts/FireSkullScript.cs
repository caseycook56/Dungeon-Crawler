using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullScript : MonoBehaviour
{
    // indexes for sprite index
    private int currSpriteIndex = 0;
    private int maxSpriteIndex;

    // decides when to animate sprites
    private int animateLoopCount = 0;
    private int animateLoopMax = 20;

    // sides of map
    private float leftEdge;
    private float rightEdge;
    private float northEdge;
    private float southEdge;

    private float xPos;
    private float xSize;

    private float yPos;
    private float ySize;

    // map info
    private float pixelsToUnits;
    private float tileSize;


    // movement of monster
    private float move = 0.3f;

    private bool flipRight = true;

    // when to move skull
    private int countToMove = 0;
    private int maxMovement = 70;

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private Direction enemyDirection = Direction.Right;

    private Sprite[] sprites;



    // Start is called before the first frame update
    void Start()
    {
        loadInfo();
    }

    private void loadInfo()
    {
        //load map info
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;
        leftEdge = GameManagerScript.Instance.LeftEdge;
        rightEdge = GameManagerScript.Instance.RightEdge;

        northEdge = GameManagerScript.Instance.NorthEdge;
        southEdge = GameManagerScript.Instance.SouthEdge;

        xSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        ySize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;

        sprites = Resources.LoadAll<Sprite>("Artwork/fire-skull");

        move *= (float)System.Math.Pow((double)GameManagerScript.Instance.ScaleSize, 2.0d);

        maxSpriteIndex = sprites.Length-1;
        tileSize = GameManagerScript.Instance.TileSize;
        changeEnemyDirection();
    }

    // change the direction of enemny
    private void changeEnemyDirection()
    {
        int random = Random.Range(0, 4);

        findPlayer();

     if (enemyDirection==Direction.Left)
        {
            flipAnimation(!flipRight);
        }
        else if (enemyDirection == Direction.Right)
        {
            flipAnimation(flipRight);
        }
    }


    // flips sprite for animation
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
        // decides when to move the skull
        if (Random.Range(0, 10) < 8)
        {
            
            countToMove++;

            if (countToMove == maxMovement)
            {
                moveDirection();
                countToMove = 0;
            }
            animateEnemy();
        }
    }

    // moves firs skull in direction of player
    private void moveDirection()
    {
        changeEnemyDirection();
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;


        // move left
        if (enemyDirection == Direction.Left)
        {
            if (xPos - xSize > leftEdge)
            {
                gameObject.transform.position += Vector3.left * move;
            }
        }

        // moves right
        else if (enemyDirection == Direction.Right)
        {
            if (xPos + xSize < rightEdge)
            {
                gameObject.transform.position += Vector3.right * move;
            }
        }

        // moves up
        else if (enemyDirection == Direction.Up)
        {
            if (yPos + ySize < northEdge)
            {
                gameObject.transform.position += Vector3.up * move;
            }
        }

        // moves down
        else if (enemyDirection == Direction.Down)
        {
            if (yPos + ySize > southEdge+ 4 * tileSize / pixelsToUnits)
            {
                gameObject.transform.position += Vector3.down * move;
            }
        }
    }

    // animate fire skull
    private void animateEnemy()
    {
        // decides when to animate the skull
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
        Debug.Log("fire skull hit");
        if (collision.CompareTag("Player"))
        {
            // fire skull does two points of damage to player
            GameManagerScript.Instance.Hearts -= 2;
        }
    }


    // finds layer so monster can move towards it
    private void findPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");

        Vector3 playerPosition = player.transform.position;

        Vector3 monsterPostion = this.gameObject.transform.position;

        // different betwen player and monster
        float x =  playerPosition.x - monsterPostion.x;
        float y = playerPosition.y- monsterPostion.y ;

        // implementation of best first search
        // which is uses a heuristic find the next direction to go in
        // in this case it is the coordients, as we know the position of each object on the field
        // i would like to change to a more advanced search if I have time, 
        //such a A* search, it's dijsktra algorithm and best first search together

        if (y>=0 && y>=x)
        {
            enemyDirection = Direction.Up;
            
        } else if(x >=0 && x >y)
        {
            enemyDirection = Direction.Right;
        }
        else if (y<0 && y <=x)
        {
            enemyDirection = Direction.Down;

        } else if(x<=0 && x<=y)
        {
            enemyDirection = Direction.Left;
        }
        
        
    }
}
