using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image onSightFill;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    private float guardAwareness;

    private void Awake()
    {
        onSightFill.fillAmount = 0f;
    }

    private void Update()
    {
        guardAwareness = PlayerBehavior.instance.GetGuardAwareness();
        AwarenessLevel(guardAwareness);
        GuardAware();
    }

    private void GuardAware()
    {
        if (onSightFill.fillAmount == 1)
        {
            GameManager.instance.GameOver();
        }
    }

    private void AwarenessLevel(float currentAWareness)
    {
        onSightFill.fillAmount = currentAWareness;
    }

    public void UpdateScore (int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }
}
