using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(runStart);

    }

    // reset button on game over screen
    private void runStart()
    {
        GameManagerScript.Instance.Reset();
        Debug.Log("reset button");
        SceneManager.LoadScene("levelScene");
    }




// Update is called once per frame
void Update()
    {
        
    }
}
