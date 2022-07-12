using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    private  bool gameIsPaused= false;

    // currently just exits game on escape button
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //working on getting the game paused
    private void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
