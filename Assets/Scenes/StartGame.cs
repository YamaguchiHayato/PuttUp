using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        for (int i = 1; i <= 12; i++)
        {
            PlayerPrefs.DeleteKey($"Score_Goal_{i}");
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("Jam");
    }
}
