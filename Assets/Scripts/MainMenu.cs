using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        GlobalData.nextRace = "Track 1";
        GlobalData.MaxSpeed = 50;
        GlobalData.Acceleration = 10;
        GlobalData.coins = 5;
        SceneManager.LoadScene("Track 1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
