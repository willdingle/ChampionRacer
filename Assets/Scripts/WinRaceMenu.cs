using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinRaceMenu : MonoBehaviour
{
    public void NextRaceButton()
    {
        SceneManager.LoadScene(GlobalData.nextRace);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
