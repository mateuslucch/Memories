using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            LoadMainMenu();
        }
    }

    public void LoadPreviosScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void LoadMainMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Start Menu");
      
    }
        public void LoadGameScene()
        {
          
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;            
            SceneManager.LoadScene("Level1");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ReturnToGame()
        {
           
        }

        public void ManualScene()
        {
            SceneManager.LoadScene("Manual Scene");
        }

    }