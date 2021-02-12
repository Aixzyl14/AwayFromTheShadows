using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentScene;
    GameObject[] Players;
    // Start is called before the first frame update
    void Start()
    {
       currentScene = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<AudioManager>().Play("MenuSong");
    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        if (currentScene == 0)
        {


            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            if(Players.Length == 0)
            {
                Invoke("GameRestart", 2f);
            }
        }
    }

    private void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
}
