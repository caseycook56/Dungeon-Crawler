using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScript : MonoBehaviour
{
    
    // indexes for sprite array
    private int animateLoopCount = 0;
    private int animateLoopMax = 20;

    // edges of map
    private float leftEdge;
    private float rightEdge;
    private float northEdge;
    private float southEdge;

    // postion of enemy
    private float xPos;
    private float xSize;

    private float yPos;
    private float ySize;

    private float pixelsToUnits;

    // movement speed of enemy
    private float move = 0.5f;
    
    

    private bool flipRight = true;

    private int countMove = 0;
    private int moveLength = 15;
    private float tileSize;


    private int countIdle = 0;
    private int idleLength = 1000;

    private int countCharge = 0;
    private int chargeLength = 150;

    private int idleIndex = 4;
    private int firstIdleIndex = 4;
    private int lastIdleIndex = 7;

    private int moveIndex = 0;
    private int firstMoveIndex = 0;
    private int lastMoveIndex = 3;

    private bool isIdle = true;

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private Direction enemyDirection = Direction.Right;

    private List<Sprite> sprites = new List<Sprite>();



    // Start is called before the first frame update
    void Start()
    {
        loadInfo();
    }

    //load map info
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
        loadSprites();

        move *= (float)System.Math.Pow((double)GameManagerScript.Instance.ScaleSize, 2.0d);
        changeEnemyDirection();
    }


    //load sprites for animation
    private void loadSprites()
    {
        sprites.Add(Resources.Load<Sprite>("Artwork/horse1"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse2"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse3"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse4"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse-idle-1"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse-idle-2"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse-idle-3"));
        sprites.Add(Resources.Load<Sprite>("Artwork/horse-idle-4"));
    }

    private void changeEnemyDirection()
    {
       
        findPlayer();

        // depending on direction moves animation of the monster
        if (enemyDirection == Direction.Left)
        {
            flipAnimation(!flipRight);
        }
        else if (enemyDirection == Direction.Right)
        {
            flipAnimation(flipRight);
            
        }
    }

    private void flipAnimation(bool flip)
    {
        if (flip==true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Debug.Log("flip right");
       
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("flip left");
        }
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Direction: " + enemyDirection);
        // the horse it idle for a time and then it charges at the player
        if (countIdle == idleLength)
        {
            isIdle = false;

            // decids if the horse is ready to charge
            if(countCharge!= chargeLength)
            {
                countCharge++;
                
                // decides if it will move
                if(countMove== moveLength)
                {
                    countMove = 0;
                    moveDirection();
                }
                else
                {
                    countMove++;
                }
            }
            else
            {
                // resets values, horse back to idle
                countIdle =0;
                countCharge = 0;
                isIdle = true;
            }
        }
        else
        {
            changeEnemyDirection();
            countIdle++;
        }
        animateEnemy();
    }


    // moves flaming horse in a direction
    private void moveDirection()
    {
        
        // current positoin of monster
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;

        // moves in a direcion until it can't move anymore

        // move left
        if (enemyDirection == Direction.Left)
        {
            
            if (xPos - xSize > leftEdge)
            {
                gameObject.transform.position += Vector3.left * move;

            }
            else
            {
                countIdle = 0;
                countCharge = 0;
                isIdle = true;
            }

        }

        //move right
        else if (enemyDirection == Direction.Right)
        {
            if (xPos + xSize < rightEdge)
            {
                gameObject.transform.position += Vector3.right * move;
            }
            else
            {
                countIdle = 0;
                countCharge = 0;
                isIdle = true;
            }

        }

       // move up
        else if (enemyDirection == Direction.Up)
        {
            if (yPos + ySize < northEdge)
            {
                gameObject.transform.position += Vector3.up * move;
            }
            else
            {
                countIdle = 0;
                countCharge = 0;
                isIdle = true;
            }
        }
        // move down
        else if (enemyDirection == Direction.Down)
        {
            if (yPos + ySize > southEdge +5 * tileSize / pixelsToUnits)
            {
                
                gameObject.transform.position += Vector3.down * move;
            }
            else
            {
                countIdle = 0;
                countCharge = 0;
                isIdle = true;
            }


        }
    }

    private void animateEnemy()
    {

        if (animateLoopCount == animateLoopMax)
        {
            animateLoopCount = 0;

            // animate idle or moving animation
            if (isIdle)
            {
                //animate idle
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[idleIndex];
                idleIndex++;

                if (idleIndex > lastIdleIndex)
                {
                    idleIndex = firstIdleIndex;
                }
            }
            else
            {
                // anaime moving animation
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[moveIndex];
                moveIndex++;

                if (moveIndex > lastMoveIndex)
                {
                    moveIndex = firstMoveIndex;
                }
            }
        }
        else
        {
            animateLoopCount++;
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Horse hit");
        if (collision.CompareTag("Player"))
        {
            // horse does 3 points of damage to player
            GameManagerScript.Instance.Hearts -= 3;
        }
    }


    // find player so the monster can move towards it
    private void findPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            Vector3 monsterPostion = this.gameObject.transform.position;

            // monster in relation to player
            float x = playerPosition.x - monsterPostion.x;
            float y = playerPosition.y - monsterPostion.y;


            // implementation of best first search
            // which is uses a heuristic find the next direction to go in
            // in this case it is the coordients, as we know the position of each object on the field
            // i would like to change to a more advanced search if I have time, 
            //such a A* search, it's dijsktra algorithm and best first search together

            if (y >= 0 && y >= x)
            {
                enemyDirection = Direction.Up;
            }
            else if (x >= 0 && x > y)
            {

                enemyDirection = Direction.Right;
            }
            else if (y < 0 && y <= x)
            {

                enemyDirection = Direction.Down;

            }
            else if (x <= 0 && x <= y)
            {

                enemyDirection = Direction.Left;

            }
            else
            {
                Debug.Log("error");
            }

           
        }


    }
}


