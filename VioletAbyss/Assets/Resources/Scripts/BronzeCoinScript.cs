using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeCoinScript : MonoBehaviour
{
    private int score;

    

    // sprite array for coin sprites
    private Sprite[] sprites;

    // current sprite in array
    private int currIndex = 0;

    // max index of array
    private int maxIndex = 7;

    
    private int loopCount = 0;

    //went to change animation
    private int loopMax = 5;

    private void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Artwork/coin_gold");
        
    }

    // update coin animation
    private void Update()
    {
        // animates the coins at different times
        int numRandom = Random.Range(0, 10);
        if (numRandom < 5)
        {
            //loops through each animation
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
        score = GameManagerScript.Instance.Score += 10;
        Destroy(gameObject);
    }
}
