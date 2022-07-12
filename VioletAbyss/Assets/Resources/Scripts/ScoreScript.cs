using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private RectTransform statusBox;
    private Text status;
    private float pixelsToUnits;

        // Start is called before the first frame update
    void Start()
    {
        loadInfo();
    }

    // loads map info
    private void loadInfo()
    {
        pixelsToUnits = GameManagerScript.Instance.PixelsToUnits;
        statusBox = gameObject.GetComponent<RectTransform>();
        Vector2 scoreSize = statusBox.sizeDelta;

        float xPos = -(Screen.width * 0.5f) + (0.5f * scoreSize.x);
        float yPos = -(Screen.height * 0.5f) + (0.5f * scoreSize.y);

        // loads text box to update player status in
        statusBox.localPosition -= new Vector3(0, yPos, 0);
        status = gameObject.GetComponent<Text>();
        status.fontSize = (int) (status.fontSize * GameManagerScript.Instance.ScaleSize);
    }

    // Update is called once per frame
    void Update()
    {
        // updates player 's status, score, level, health
       
        status.text = "  SCORE:" + GameManagerScript.Instance.Score
        + "    HEARTS: " + GameManagerScript.Instance.Hearts + "/" + GameManagerScript.Instance.MaxHearts
        +"     LEVEL: " + GameManagerScript.Instance.Level ;
        
        
    }
}
