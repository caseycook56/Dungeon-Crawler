using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{

    // enemey info
    private GameObject enemy;
    private GameObject ghostPrefab;
    private GameObject skeletonPrefab;
    private GameObject fireSkullPrefab;
    private GameObject horsePrefab;

    // map info
    private float tileSize;
    private float scaleSize;
    private int numRow;
    private int numCol;
    private float pixelsToUnits;

    // Start is called before the first frame update
    void Start()
    {
        generateEnemy();
    }

    private void generateEnemy()
    {
        loadInfo();
        int level = GameManagerScript.Instance.Level;

        //create ghosts
        for (int i=0; i < 1 + (level / 3) && i<5; i++)
        {
            createEnemy(ghostPrefab);
        }

        //create skeletons
        for (int i = 0; i < (level / 5) && i<5; i++)
        {
            createEnemy(skeletonPrefab);
        }

        // create flaming skull
        if(level<13 || level > 17)
        {
            for (int i = 0; i < level / 9; i++)
            {
                createEnemy(fireSkullPrefab);
            }
        }

        // create flaming horse
        for (int i = 0; i < level / 13; i++)
        {
            createEnemy(horsePrefab);
        }
    }

    private void loadInfo()
    {
    // load enemies
        ghostPrefab = Resources.Load("Prefabs/Ghost") as GameObject;
        skeletonPrefab = Resources.Load("Prefabs/Skeleton") as GameObject;
        fireSkullPrefab = Resources.Load("Prefabs/FireSkull") as GameObject;
        horsePrefab = Resources.Load("Prefabs/FlamingHorse") as GameObject;

        // load map info
        tileSize = GameManagerScript.Instance.TileSize;
        numRow = GameManagerScript.Instance.Row - 1;
        numCol = GameManagerScript.Instance.Col;
        scaleSize = GameManagerScript.Instance.ScaleSize;
        pixelsToUnits= GameManagerScript.Instance.PixelsToUnits;

    }

    // creates and place emeny on the map
    private void createEnemy(GameObject enemyType)
    {
        int randomRow;
        int randomCol;

        float yPos;
        float xPos;

        randomRow = Random.Range(0, numRow);
        randomCol = Random.Range(0, numCol);

        // doesn't spawn enemy next to the player
        if((randomRow <3  && randomCol < 5) || randomRow==0 || randomCol==0)
        {
            // if does try to spawn enemy next to the player it calls then it calls create enemy again
            createEnemy(enemyType);
        }
        else

        {
            // finds the x and y postion on the map
            yPos = (float)((Screen.height * 0.5f) - (tileSize * randomRow) - (0.5f * tileSize)) / pixelsToUnits;

            xPos = (float)(-(0.5f * Screen.width) + (tileSize * randomCol) + (0.5f * tileSize)) / pixelsToUnits;

            // creates enemy on the map
            enemy = Instantiate(enemyType);
            enemy.transform.position = new Vector3(xPos, yPos, 0);
            enemy.transform.localScale *= scaleSize;
        }

       
    }

          
}
