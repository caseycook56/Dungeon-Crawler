using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    // index for sprite array for animation
    private int currSpriteIndex = 0;
    private int maxSpriteIndex = 7;
    
    // edges of map
    private float leftEdge;
    private float rightEdge;
    private float northEdge;
    private float southEdge;

    // positon of enemy 
    private float xPos;
    private float xSize;

    private float yPos;
    private float ySize;


    private float pixelsToUnits;
    private float tileSize;

    // movement of enemy
    private float move = 0.1f;
    
    // when the direction is changed
    private int changeDirection = 250;
    private int countChangeDirection = 0;

    private bool flipRight = true;

    //when enemies moves
    private int moveEnemyCount = 0;
    private int moveEnemy = 30;

    
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    // current direction of enemy
    private Direction enemyDirection = Direction.Left;

    // sprite array for animation
    private List<Sprite> sprites = new List<Sprite>();



    // Start is called before the first frame update
    void Start()
    {
        loadInfo();
    }

    // loads map info
    private void loadInfo()
    {
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;
        leftEdge = GameManagerScript.Instance.LeftEdge;
        rightEdge = GameManagerScript.Instance.RightEdge;

        northEdge = GameManagerScript.Instance.NorthEdge;
        southEdge = GameManagerScript.Instance.SouthEdge;

        xSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        ySize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        loadSprities();
        move *= (float)System.Math.Pow((double)GameManagerScript.Instance.ScaleSize, 2.0d);
        tileSize = GameManagerScript.Instance.TileSize;
        changeEnemyDirection();
    }

    // loads sprites 
    private void loadSprities()
    {
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton1"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton2"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton3"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton4"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton5"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton6"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton7"));
        sprites.Add(Resources.Load<Sprite>("Artwork/skeleton8"));
    }


    // Update is called once per frame
    void Update()

    {
        int random = Random.Range(0, 10);
        if (random < 4)
        {
            countChangeDirection++;
            moveEnemyCount++;

            if (moveEnemyCount == moveEnemy)
            {
                moveDirection();
                animateEnemy();
                moveEnemyCount = 0;
            }

            if (countChangeDirection == changeDirection)
            {
                changeEnemyDirection();
            }
            else
            {
                countChangeDirection = 0;
            }
            
        }
     }

    // moves the enemy in a random direction
    private void moveRandom()
    {
        int random = Random.Range(0, 4);

        // move down
        if (random == 0)
        {
            enemyDirection = Direction.Down;
        }

        //move up
        else if (random == 1)
        {
            enemyDirection = Direction.Up;
        }

        // move left
        else if (random == 2)
        {
            enemyDirection = Direction.Left;

            // flips sprite for animation if it needs to
            flipAnimation(!flipRight);
        }

        // move right
        else if (random == 3)
        {
            enemyDirection = Direction.Right;
            flipAnimation(flipRight);
            
        }
    }
       

    // skeleton has a small chance of moving in the direction of the player
    private void changeEnemyDirection()
    {
        int random = Random.Range(0, 10);

        if (random > 2)
        {
            moveRandom();
        }
        else
        {
            findPlayer();

            if (enemyDirection == Direction.Left)
            {
                flipAnimation(!flipRight);
            }
            else if (enemyDirection == Direction.Right)
            {

                flipAnimation(flipRight);
            }
        }
    }

    // decides if the sprite needs to be flipped for animation
    private void flipAnimation( bool flip)
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


    // moves skeleton in the current direction
    private void moveDirection()
    {

        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;

        //move left
        if (enemyDirection == Direction.Left)
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

        //move right
        else if (enemyDirection == Direction.Right)
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
        else if (enemyDirection == Direction.Up)
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

        // moves down
        else if (enemyDirection == Direction.Down)
        {
            if (yPos + ySize >  southEdge +2 * tileSize / pixelsToUnits)
            {
                gameObject.transform.position += Vector3.down * move;
            }
            else
            {
                changeEnemyDirection();
            }


        }
    }

    // animation skeleton
    private void animateEnemy()
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("skeleton");
        if (collision.CompareTag("Player"))
        {
            // skeleton does 1 point of dmamge to player
            GameManagerScript.Instance.Hearts -= 1;

        }
    }

    // changes direction of monster towards player
    private void findPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");

        Vector3 playerPosition = player.transform.position;

        Vector3 monsterPostion = this.gameObject.transform.position;

        // moster in relation to player
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
    }
}
