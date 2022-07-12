using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverCoinScript : MonoBehaviour
{
    

    

    // sprite array for animations
    private Sprite[] sprites;

    // indexs for sprite array, to update animations
    private int currIndex = 0;
    private int maxIndex = 7;

    // decides when to update animations
    private int loopCount = 0;
    private int loopMax = 5;

    private void Start()
    {
        // load sprite aniamtions
        sprites = Resources.LoadAll<Sprite>("Artwork/coin_gold");
        
    }


    // update coin animations
    private void Update()
    {
        //has randomness, so the coins updte at different times
        if (Random.Range(0, 10) < 6)
        {
            // decides if the animations needs an update
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
        // ads 50 points to score
        GameManagerScript.Instance.Score += 50;
        Destroy(gameObject);
    }
}
