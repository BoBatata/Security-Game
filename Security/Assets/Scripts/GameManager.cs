using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameOverUI gameOver;
    [SerializeField] private PlayerBehavior playerBehavior;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UiManager uiManager;

    private int gameScore;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("sGame");
    }

    public void GameOver()
    {
        inputManager.Disable();
        gameOver.ShowGameOver();
    }

    public void IncreaseScore(int gainScore)
    {
        gameScore += gainScore;
        gameOver.GameoverBestScore(gameScore);
        uiManager.UpdateScore(gameScore);
    }
}
