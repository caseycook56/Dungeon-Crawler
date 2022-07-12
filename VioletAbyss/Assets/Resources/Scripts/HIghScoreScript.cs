using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HIghScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text score= gameObject.GetComponent<Text >(); 
        score.text = "Score: " + GameManagerScript.Instance.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
