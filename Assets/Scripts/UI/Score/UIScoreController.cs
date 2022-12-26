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
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
        ScoreManager.OnHighScoreChanged += UpdateHighScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
        ScoreManager.OnHighScoreChanged -= UpdateHighScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString("0000");
    }

    private void UpdateHighScore(int newScore)
    {
        highScoreText.text = newScore.ToString("0000");
    }
}
