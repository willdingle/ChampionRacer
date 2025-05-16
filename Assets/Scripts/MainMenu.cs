using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.F2))
        {
            GlobalData.nextRace = "Track 2";
            GlobalData.MaxSpeed = 60;
            GlobalData.FinalOppMaxSpeed = 55;
            GlobalData.Acceleration = 10;
            GlobalData.TurnSpeed = 60;
            SceneManager.LoadScene("WinRace");
        }
    }

    public void PlayButton()
    {
        GlobalData.nextRace = "Track 1";
        GlobalData.MaxSpeed = 50;
        GlobalData.Acceleration = 10;
        GlobalData.TurnSpeed = 60;
        SceneManager.LoadScene("Story Menu");
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
