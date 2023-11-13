using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitGameButton;

    private void Awake()
    {
        playButton.onClick.AddListener(GoToGameScene);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameScene()
    {
        SceneManager.LoadScene("sGame");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
