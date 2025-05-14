using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseRaceMenu : MonoBehaviour
{
    public TMP_Text coinCountText;
    public TMP_Text maxSpeedText;
    public TMP_Text accelerationText;

    // Start is called before the first frame update
    void Start()
    {
        coinCountText.text = "Coins: " + GlobalData.coins;
        maxSpeedText.text = "Max Speed: " + GlobalData.MaxSpeed;
        accelerationText.text = "Acceleration: " + GlobalData.Acceleration;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartRaceButton()
    {
        SceneManager.LoadScene(GlobalData.nextRace);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
