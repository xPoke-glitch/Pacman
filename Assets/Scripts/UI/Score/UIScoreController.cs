using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScoreController : MonoBehaviour
{
    [Header("View References")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString("0000");
    }
}
