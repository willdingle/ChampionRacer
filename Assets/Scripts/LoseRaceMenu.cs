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
    public TMP_Text handlingText;

    // Start is called before the first frame update
    void Start()
    {
        coinCountText.text = "Coins: " + GlobalData.coins;
        maxSpeedText.text = "Max Speed: " + GlobalData.MaxSpeed;
        accelerationText.text = "Acceleration: " + GlobalData.Acceleration;
        handlingText.text = "Handling: " + GlobalData.Acceleration;
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

    public void UpgradeMaxSpeedButton()
    {
        if (GlobalData.coins >= 5)
        {
            GlobalData.MaxSpeed += 10;
            maxSpeedText.text = "Max Speed: " + GlobalData.MaxSpeed;
            GlobalData.coins -= 5;
            coinCountText.text = "Coins: " + GlobalData.coins;
        }
    }

    public void UpgradeAccelerationButton()
    {
        if (GlobalData.coins >= 5)
        {
            GlobalData.Acceleration += 5;
            accelerationText.text = "Acceleration: " + GlobalData.Acceleration;
            GlobalData.coins -= 5;
            coinCountText.text = "Coins: " + GlobalData.coins;
        }
    }

    public void UpgradeHandlingButton()
    {
        if (GlobalData.coins >= 5)
        {
            GlobalData.TurnSpeed += 10;
            handlingText.text = "Handling: " + GlobalData.TurnSpeed;
            GlobalData.coins -= 5;
            coinCountText.text = "Coins: " + GlobalData.coins;
        }
    }
}
