using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private int score;

    // 1: public variable for sprites
    //public Sprite[] sprites;

    // 2: private array
    private Sprite[] sprites;

    private int currIndex = 0;
    private int maxIndex = 7;

    private int loopCount = 0;
    private int loopMax = 5;

    private void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Artwork/coin_gold");
        
    }

    // animates coins
    private void Update()
    {
        // animates the coins at different times
        int numRandom = Random.Range(0, 10);
        if (numRandom < 5)
        {
            if (loopCount == loopMax)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currIndex];
                if (currIndex < maxIndex)
                {
                    currIndex++;
                }
                else
                {
                    currIndex = 0;

                }
                    
                loopCount = 0;
            }
            else
            {
                loopCount++;
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score = GameManagerScript.Instance.Score += 100;
        
        Destroy(gameObject);
    }
}
