using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image onSightFill;
    private float guardAwareness;

    private void Awake()
    {
        onSightFill.fillAmount = 0f;
    }

    private void Update()
    {
        guardAwareness = PlayerBehavior.instance.GetGuardAwareness();
        AwarenessLevel(guardAwareness);
    }

    private void AwarenessLevel(float currentAWareness)
    {
        onSightFill.fillAmount = currentAWareness;
    }
}
