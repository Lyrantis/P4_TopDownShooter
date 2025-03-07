using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Canvas canvas;

    public void StartGame()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
    public void GameOver()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void Win()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
