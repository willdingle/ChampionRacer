using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGraphicsQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }


}
