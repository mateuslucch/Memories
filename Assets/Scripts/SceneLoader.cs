using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string level = "Addlevelscenename";
    [SerializeField] string startMenu = "AddStartMenuscenename";

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            print("teste");
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(startMenu);
    }

    public void LoadGameScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
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