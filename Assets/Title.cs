using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        //{
        //    // Load the next scene
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        //}
    }

    public void StartButton()
    {
        //// Load the next scene
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Debug.Log("Start");
        SceneManager.LoadScene("Jam");
    }
}
