using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void quit()
    {
        
        Application.Quit();
       
    }
}
