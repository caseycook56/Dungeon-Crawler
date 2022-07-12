using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnMapScript : MonoBehaviour
{

    private GameObject hazard;
    private GameObject floor1;
    private GameObject portal;
    private GameObject floor2;
    private GameObject floor3;
    private GameObject floor4;
    private GameObject floor5;
    private GameObject floor6;
    private GameObject floor7;
    private GameObject floor8;
    private GameObject wall;


    private int level;
    

    void Awake()
    {

    }

    // if needed to debug in bulid mode, need to print to the screen
    private void adddText(string addText)
    {
        Text scoreText;
        scoreText = gameObject.GetComponent<Text>();
        scoreText.text += addText;
    }


    //resizes the map to fit the screen
    //so it works in bulid mode

    private void scaleToScreen()
    {
        loadObjects();
        
        float pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;

        int maxRow = 20;
        int maxCol = 30;

        int numRow;
        int numCol;

        //finds default tile size
        portal = (GameObject)Resources.Load("Prefabs/Ladder");
        float defaultTileSize = portal.GetComponent<SpriteRenderer>().sprite.bounds.size.x * pixelsToUnits * 2;

        //the amount that all objects need to be scaled to when they are created
        float scalesize = 1;


        float tileSize;


        // decides if the deafult size of the tiles needs to be scaled up or not
        if ((Screen.height / maxRow) > defaultTileSize)
        {
            // rescales tile size
            tileSize = Screen.height / maxRow;
            scalesize = tileSize / defaultTileSize;
        }
        else
        {
            //uses deafult tile size
            tileSize = defaultTileSize;
        }

        
        Debug.Log("!!!!!!New Tile size: "+ tileSize);
        Debug.Log("!default Tile size: " + defaultTileSize);

        //stores new scale size;
        GameManagerScript.Instance.ScaleSize = scalesize;

        //stores new tile size
        GameManagerScript.Instance.TileSize = tileSize;


        // finds the the amount of columns in the map
        if (Screen.width / tileSize < maxCol)
        {
            numCol = (int)(Screen.width / tileSize);

        }
        else
        {
            numCol = maxCol;
        }

        
        // finds the amount of rows in a map
        if (Screen.height / tileSize < maxRow)
        {
            numRow = (int)(Screen.height / tileSize);
        }
        else
        {
            numRow = maxRow;
        }

        //minus one so that the player status at the bottom isn't over the map
        numRow -= 1;

        //stores new row and col
        GameManagerScript.Instance.Row = numRow;
        GameManagerScript.Instance.Col = numCol;

        Debug.Log("current col" + numCol);


        // finds random position for the portal(ladder)
        int portalCol = GameManagerScript.Instance.PortalRow = Random.Range(2, numCol - 2);
        int portalRow = GameManagerScript.Instance.PortalCol = Random.Range(2, numRow - 2);

        float xPos;
        float yPos;
        int random;

        GameManagerScript.Instance.findEdges();
        // creates map
        for (int row = 0; row < numRow; row++)
        {
            // get y postion of curren cell
            yPos = (float)((Screen.height * 0.5f) - (tileSize * row) - (0.5f * tileSize)) / pixelsToUnits;
            // Loop through cols
            for (int col = 0; col < numCol; col++)
            {

                // get x postion of current cell
                xPos = (float)(-(0.5f * Screen.width) + (tileSize * col) + (0.5f * tileSize)) / pixelsToUnits;

                // moves players to 1,1 cell of the map
                if (row == 1 && col == 1)
                {
                    GameObject player = GameObject.FindWithTag("Player");
                    
                    player.transform.position = new Vector3(xPos, yPos, 0);

                    if (level == 1)
                    {
                        player.transform.localScale *= scalesize;

                    }
                }

                //add walls to map
                addWalls(row, col, numRow, numCol, scalesize, xPos, yPos);

                //adds portal to level
                if (row == portalRow && col == portalCol)
                {
                    createTile(portal, scalesize, xPos, yPos);
                }
                else
                {
                    //decides how many hazards to put down based on the level
                    // the higher the level the more hazards (spikes)
                    int percent;

                    if (10 - (level / 3) > 3)
                    {
                        percent = 11 - level / 3;
                    }
                    else
                    {
                        percent = 3;
                    }

                    random = Random.Range(0, percent);

                    // doesn't but hazards around the player spawn or the edges of the map
                    if (random == 0 && (row > 3 || col > 3) && row != numRow - 1 && col != numCol - 1 && col != 0 && row != 0)
                    {
                        createTile(hazard, scalesize, xPos, yPos);
                    }
                    else
                    {
                        // if there is no hazard on the square there is just ordinary floor space
                        addFloor(scalesize, xPos, yPos);
                    }
                }
            }
        }
    }

    private void createItems()
    {
        Instantiate((GameObject)Resources.Load("Prefabs/spawnCoin"));
        Instantiate((GameObject)Resources.Load("Prefabs/spawnEnemy"));
    }


    private void addWalls(int row, int col, int numRow, int numCol, float scalesize, float xPos, float yPos){

        // decides what wall type to add

        //left wall
        if (col == 0)
        {
            createTile(wall, scalesize, xPos, yPos);
        }

        //right wall
        if (col == numCol - 1)
        {
            createTile(wall, scalesize, xPos, yPos, 180);
        }

        //top wall
        if (row == 0)
        {
            createTile(wall, scalesize, xPos, yPos, -90);
        }

        // bottom wall
        if (row == numRow - 1)
        {
            createTile(wall, scalesize, xPos, yPos, 90);
        }
        
    }

    private void loadObjects()
    {
        hazard = (GameObject)Resources.Load("Prefabs/Spikes");

        floor1 = (GameObject)Resources.Load("Prefabs/Floor1");

        floor2 = (GameObject)Resources.Load("Prefabs/Floor2");
        floor3 = (GameObject)Resources.Load("Prefabs/Floor3");
        floor4 = (GameObject)Resources.Load("Prefabs/Floor4");
        floor5 = (GameObject)Resources.Load("Prefabs/Floor5");
        floor6 = (GameObject)Resources.Load("Prefabs/Floor6");
        floor7 = (GameObject)Resources.Load("Prefabs/Floor7");
        floor8 = (GameObject)Resources.Load("Prefabs/Floor8");

        wall = (GameObject)Resources.Load("Prefabs/WallLeftSide");
        level = GameManagerScript.Instance.Level;
    }

    // moves camera to that its centers on the map
    private void changeCamera()
    {

        float xLength = (GameManagerScript.Instance.TileSize * GameManagerScript.Instance.Col) / GameManagerScript.Instance.PixelsToUnits;
        float yLength = (GameManagerScript.Instance.TileSize * GameManagerScript.Instance.Row) / GameManagerScript.Instance.PixelsToUnits;

        float xScreen = Screen.width / GameManagerScript.Instance.PixelsToUnits;
        float yScreen = Screen.height / GameManagerScript.Instance.PixelsToUnits;

        //difference between camera and map size
        float xMove = xScreen/2 - xLength/2;
        float yMove = yScreen/2 - yLength / 2;

        
        Camera.main.transform.position+= new Vector3(-xMove, yMove,0);
    }
   


    private void addFloor(float scalesize, float xPos, float yPos)
    {
        //decides what kind of floor asset to add
        int random = Random.Range(0, 20);
        if (random == 1)
        {
            createTile(floor2, scalesize, xPos, yPos);

        }
        else if (random == 2)
        {
            createTile(floor3, scalesize, xPos, yPos);

        }
        else if (random == 3)
        {
            createTile(floor4, scalesize, xPos, yPos);

        }
        else if (random == 4)
        {
            createTile(floor5, scalesize, xPos, yPos);

        }
        else if (random == 5)
        {
            createTile(floor6, scalesize, xPos, yPos);

        }
        else if (random == 6)
        {
            createTile(floor7, scalesize, xPos, yPos);

        }
        else if (random == 7)
        {
            createTile(floor8, scalesize, xPos, yPos);

        }
        else
        {
            createTile(floor1, scalesize, xPos, yPos);

        }

    }

    // creates a object (mostly tiles) and adds them on the map to scale
    private void createTile(GameObject tile, float scalesize, float x, float y)
    {
        GameObject newTile = Instantiate(tile);
        newTile.transform.localScale *= scalesize;
        newTile.transform.position = new Vector3(x, y, 0);
    }

    //screates tile and adds rotation
    private void createTile(GameObject tile, float scalesize, float x, float y, float rotate)
    {
        GameObject newTile = Instantiate(tile);
        newTile.transform.localScale *= scalesize;
        newTile.transform.position = new Vector3(x, y, 0);
        newTile.transform.Rotate(0, 0, rotate);
    }






    

    
    // Start is called before the first frame update
    void Start()
    {
        scaleToScreen();
        changeCamera();
        createItems();

      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
