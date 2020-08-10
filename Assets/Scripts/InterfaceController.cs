using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public Slider LifeSlider;
    public GameObject GameOverPanel;
    public Text TextSurvivalTime;
    public Text TextBestSurvivalTime;

    private PlayerController playerController;
    private float bestSavedScore = 0;


    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        LifeSlider.maxValue = playerController.Status.Life;
        UpdateLifeSlider();
        Time.timeScale = 1;
        bestSavedScore = PlayerPrefs.GetFloat("BestScore");
    }

    public void UpdateLifeSlider()
    {
        LifeSlider.value = playerController.Status.Life;
    }

    void UpdateBestScore()
    {
        if (Time.timeSinceLevelLoad > bestSavedScore)
        {
            bestSavedScore = Time.timeSinceLevelLoad;
        }

        string text = string.Format("O recorde é de {0}", string.Format("{0}:{1:00}", (int)bestSavedScore / 60, (int)bestSavedScore % 60));
        TextBestSurvivalTime.text = text;
        PlayerPrefs.SetFloat("BestScore", bestSavedScore);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        float timer = Time.timeSinceLevelLoad;

        TextSurvivalTime.text = string.Format("Você sobreviveu por {0}", string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60));
        UpdateBestScore();
    }

    public void Restart()
    {
        SceneManager.LoadScene("game");
    }
}
