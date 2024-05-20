using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Level",0)!= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
