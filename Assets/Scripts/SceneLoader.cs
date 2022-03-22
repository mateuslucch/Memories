using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string level = "Addlevelscenename";
    [SerializeField] string startMenu = "AddStartMenuscenename";
    [SerializeField] string credits = "Add credits scene name";
    [SerializeField] GameObject loadingScreen = null;

    private void Start()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {            
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        print("teste");
        SceneManager.LoadScene(startMenu);
    }

    public void LoadGameScene()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(credits);
    }
}