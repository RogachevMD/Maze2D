using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("SliderScene");
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
