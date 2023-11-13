using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private void Awake()
    {
        replayButton.onClick.AddListener(PlayAgain);
        homeButton.onClick.AddListener(BackMenu);
    }

    private void PlayAgain()
    {
        GameManager.instance.ReplayGame();
    }

    private void BackMenu()
    {
        GameManager.instance.GoBackMenu();
    }
    
    public void GameoverBestScore(int bestScore)
    {
        gameOverScoreText.text = "Score: " + bestScore.ToString();
    }

    public void ShowGameOver()
    {
        this.gameObject.SetActive(true);
    }
}
