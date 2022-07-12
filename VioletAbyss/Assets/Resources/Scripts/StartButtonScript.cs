using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(runStart);
        
    }

    // starts game from title screen 
    private void runStart()
    {
        Debug.Log("start button");
        SceneManager.LoadScene("levelScene");
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
