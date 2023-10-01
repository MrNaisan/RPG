using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FlyScene");
    }

    public void Level1()
    {
        SceneManager.LoadScene("FlyScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}