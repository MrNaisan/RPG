using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public Toggle MusicToggle;
    public Toggle SoundsToggle;

    private void Start() 
    {
        if(PlayerPrefs.GetInt("BackGroundSound") == 0)
            PlayerPrefs.SetInt("BackGroundSound", 1);
        if(PlayerPrefs.GetInt("SoundEffects") == 0)
            PlayerPrefs.SetInt("SoundEffects", 1);

        MusicToggle.isOn = PlayerPrefs.GetInt("BackGroundSound") == 1 ? true : false;
        SoundsToggle.isOn = PlayerPrefs.GetInt("SoundEffects") == 1 ? true : false;
    }

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

    public void Back()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }

    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void Music()
    {
        PlayerPrefs.SetInt("BackGroundSound", MusicToggle.isOn ? 1 : 2);
    }

    public void Sounds()
    {
        PlayerPrefs.SetInt("SoundEffects", SoundsToggle.isOn ? 1 : 2);
    }
}
