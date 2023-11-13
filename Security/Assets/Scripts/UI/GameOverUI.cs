using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private void Awake()
    {
        replayButton.onClick.AddListener(PlayAgain);
    }

    private void PlayAgain()
    {
        GameManager.instance.ReplayGame();
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
