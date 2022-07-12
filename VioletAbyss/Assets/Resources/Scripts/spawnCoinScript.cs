using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spawnCoinScript : MonoBehaviour
{
    private GameObject bronzeCoinPrefab;
    private GameObject silverCoinPrefab;
    private GameObject goldCoinPrefab;
    private GameObject heartsPrefab;
    private GameObject heartContainerPrefab;
    private GameObject speedPotionPrefab;
    private GameObject invincibilityPotionPrefabb;


    private float pixelsToUnits;
    

    private int numCol;
    private int numRow;
    private float tileSize;
    private int level;
    private float scaleSize;
    

    private void loadItems()
    {
        //loads items
        bronzeCoinPrefab = (GameObject)Resources.Load("Prefabs/BronzeCoin");
        silverCoinPrefab = (GameObject)Resources.Load("Prefabs/SilverCoin");
        goldCoinPrefab = (GameObject)Resources.Load("Prefabs/GoldCoin");
        heartsPrefab = (GameObject)Resources.Load("Prefabs/Hearts");
        heartContainerPrefab = (GameObject)Resources.Load("Prefabs/HeartContainers");
        speedPotionPrefab = (GameObject)Resources.Load("Prefabs/SpeedPotion");
        invincibilityPotionPrefabb = (GameObject)Resources.Load("Prefabs/InvincibilityPotion");

        //loads map information
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;
        level = GameManagerScript.Instance.Level;
        numCol = GameManagerScript.Instance.Col;
        numRow = GameManagerScript.Instance.Row;
        tileSize = GameManagerScript.Instance.TileSize;
        scaleSize = GameManagerScript.Instance.ScaleSize;

    }

    private void spawnItems()
    {

        loadItems();
        
        float xPos=0;
        float yPos=0;
        int random=0;

        int hearts = 0;

        bool heartContainer = false;
        bool speedPotion = false;
        bool invincibilityPotion = false;


        // items across map
        for (int row = 1; row < numRow - 1; row++)
        {

            // get Y position 
            yPos = (float)((Screen.height * 0.5f) - (tileSize * row) - (0.5f * tileSize)) / pixelsToUnits;
            
            for (int col = 1; col < numCol - 1; col++)
            {
                //Debug.Log("portal row:" + GameManagerScript.Instance.PortalRow);
                //Debug.Log("portal col:" + GameManagerScript.Instance.PortalCol);


                // if the cell isnt the portal cell, an item might be  placed
                if (row != GameManagerScript.Instance.PortalRow && col != GameManagerScript.Instance.PortalCol || 1 == 0)
                {


                    // get X postion  and place coin
                    xPos = (float)(-(0.5f * Screen.width) + (tileSize * col) + (0.5f * tileSize)) / pixelsToUnits;

                    //decides how many coins to place down
                    int currentPercent = 30;

                    int percent = currentPercent - 10 * (int)System.Math.Log(level);

                    if (percent < 4)
                    {
                        random = Random.Range(0, 4);
                    }
                    else
                    {
                        random = Random.Range(0, percent);
                    }



                    if (random == 1)
                    {
                        // level 5 and under only bronze coins
                        if (level < 5)
                        {
                            createItem(bronzeCoinPrefab, xPos, yPos);

                        }
                        // level 5 -9 only bronze, and silver coins
                        else if (level > 4 & level < 10)
                        {
                            random = Random.Range(0, 2);
                            if (random == 0)
                            {
                                createItem(bronzeCoinPrefab, xPos, yPos);
                            }
                            else
                            {
                                createItem(silverCoinPrefab, xPos, yPos);
                            }

                        }
                        // all coins on level 10 onwards
                        else if (level >= 10)
                        {
                            random = Random.Range(0, 3);
                            if (random == 0)
                            {
                                createItem(bronzeCoinPrefab, xPos, yPos);
                            }
                            else if (random == 1)
                            {

                                createItem(silverCoinPrefab, xPos, yPos);
                            }
                            else
                            {

                                createItem(goldCoinPrefab, xPos, yPos);
                            }
                        }

                    }
                    // decides if a heart gets placed
                    else if (random == 2)
                    {
                        random = Random.Range(0, 5);
                        if (random == 1 && hearts < 1 + level / 3)
                        {
                            hearts++;
                            createItem(heartsPrefab, xPos, yPos);
                        }
                    }
                    // decides if a heart container ( increase max health potion) gets placed
                    else if (random == 3)
                    {
                        random = Random.Range(0, 5);

                        // has a chance placing a heart contianer every 5 levels 
                        if (random == 1 && level % 5 == 0 && heartContainer == false)
                        {
                            heartContainer = true;
                            createItem(heartContainerPrefab, xPos, yPos);
                        }
                    }

                    //decides if a speed potion is to be placed
                    else if (random == 4)
                    {
                        // appears on level 3
                        if (level > 3 && speedPotion == false)
                        {
                            speedPotion = true;

                            createItem(speedPotionPrefab, xPos, yPos);
                        }
                    }

                    //decides if a invinciblity potion is to be placed
                    else if (random == 5)
                    {
                        // appears from level 7 onwards
                        if (level > 6 && invincibilityPotion == false)
                        {
                            invincibilityPotion = true;

                            createItem(invincibilityPotionPrefabb, xPos, yPos);
                        }


                    }

                }
                

            }

        }
    }


    //old way of spawning items
    // not in use
    public void oldItems()
    {
        Debug.Log("spawning items");



        GameObject bronzeCoinPrefab = (GameObject)Resources.Load("Prefabs/BronzeCoin");
        GameObject silverCoinPrefab = (GameObject)Resources.Load("Prefabs/SilverCoin");
        GameObject goldCoinPrefab = (GameObject)Resources.Load("Prefabs/GoldCoin");
        GameObject heartsPrefab = (GameObject)Resources.Load("Prefabs/Hearts");
        GameObject heartContainerPrefab = (GameObject)Resources.Load("Prefabs/HeartContainers");

        GameObject speedPotionPrefab = (GameObject)Resources.Load("Prefabs/SpeedPotion");
        GameObject invincibilityPotionPrefabb = (GameObject)Resources.Load("Prefabs/InvincibilityPotion");



        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;

        GameObject coin;

        int level = GameManagerScript.Instance.Level;



        tileSize = GameManagerScript.Instance.TileSize;

        GameManagerScript.Instance.Col = (int)(Screen.width / tileSize + 1);
        GameManagerScript.Instance.Row = (int)(Screen.height / tileSize + 1) - 2;

        float xPos;
        float yPos;
        int random;

        numCol = GameManagerScript.Instance.Col;
        numRow = GameManagerScript.Instance.Row;

        //Debug.Log("Row: " + numRow);
        //Debug.Log("Col: " + numCol);

        int hearts = 0;

        bool heartContainer = false;
        bool speedPotion = false;
        bool invincibilityPotion = false;



        for (int row = 1; row < numRow - 1; row++)
        {

            // get Y position 
            yPos = (float)((Screen.height * 0.5f) - (tileSize * row) - (0.5f * tileSize)) / pixelsToUnits;
            // Loop through cols
            for (int col = 1; col < numCol - 1; col++)
            {
                Debug.Log("portal row:" + GameManagerScript.Instance.PortalRow);
                Debug.Log("portal col:" + GameManagerScript.Instance.PortalCol);

                Debug.Log("add items");


                // get X postion  and place coin
                xPos = (float)(-(0.5f * Screen.width) + (tileSize * col) + (0.5f * tileSize)) / pixelsToUnits;

                int currentPercent = 30;

                int percent = currentPercent - 10 * (int)System.Math.Log(level);
                // Debug.Log("coin percent: " + percent);
                if (percent < 4)
                {
                    random = Random.Range(0, 4);
                }
                else
                {
                    random = Random.Range(0, percent);
                }



                if (random == 1)
                {
                    if (level < 5)
                    {
                        coin = Instantiate(bronzeCoinPrefab);
                        coin.transform.position = new Vector3(xPos, yPos, 0);

                    }
                    else if (level > 4 & level < 10)
                    {
                        random = Random.Range(0, 2);
                        if (random == 0)
                        {
                            coin = Instantiate(bronzeCoinPrefab);
                            coin.transform.position = new Vector3(xPos, yPos, 0);
                        }
                        else
                        {
                            coin = Instantiate(silverCoinPrefab);
                            coin.transform.position = new Vector3(xPos, yPos, 0);
                        }

                    }
                    else if (level >= 10)
                    {
                        random = Random.Range(0, 3);
                        if (random == 0)
                        {
                            coin = Instantiate(bronzeCoinPrefab);
                            coin.transform.position = new Vector3(xPos, yPos, 0);
                        }
                        else if (random == 1)
                        {
                            coin = Instantiate(silverCoinPrefab);
                            coin.transform.position = new Vector3(xPos, yPos, 0);
                        }
                        else
                        {
                            coin = Instantiate(goldCoinPrefab);
                            coin.transform.position = new Vector3(xPos, yPos, 0);
                        }
                    }


                }
                else if (random == 2)
                {
                    random = Random.Range(0, 5);
                    if (random == 1 && hearts < 1 + level / 3)
                    {
                        coin = Instantiate(heartsPrefab);
                        coin.transform.position = new Vector3(xPos, yPos, 0);
                        hearts++;
                        Debug.Log("add hearts");
                    }
                }
                else if (random == 3)
                {
                    random = Random.Range(0, 5);
                    if (random == 1 && level % 5 == 0 && heartContainer == false)
                    {
                        heartContainer = true;
                        coin = Instantiate(heartContainerPrefab);
                        coin.transform.position = new Vector3(xPos, yPos, 0);
                        Debug.Log("add heart container");
                    }
                }
                else if (random == 4)
                {
                    if (level > 3 && speedPotion == false)
                    {
                        speedPotion = true;
                        coin = Instantiate(speedPotionPrefab);
                        coin.transform.position = new Vector3(xPos, yPos, 0);
                    }
                }
                else if (random == 5)
                {
                    if (level > 6 && invincibilityPotion == false)
                    {
                        invincibilityPotion = true;
                        coin = Instantiate(invincibilityPotionPrefabb);
                        coin.transform.position = new Vector3(xPos, yPos, 0);
                    }

                }




            }

        }
    
}

    // creates an item and places it on the map
    private void createItem(GameObject item, float xPos, float yPos)
    {
        GameObject newItem = Instantiate(item);
        newItem.transform.position = new Vector3(xPos, yPos, 0);
        newItem.transform.localScale *= scaleSize;
    }

    

    // Start is called before the first frame update
    void Start()
    {

        spawnItems();

    }
    
}
