using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : Singleton<ScoreManager>
{
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;

    private int _totalScore = 0;
    private int _highScore = 0;
    public void AddPoint(int amount)
    {
        _totalScore += amount;
        OnScoreChanged?.Invoke(_totalScore);
    }

    public void SaveHighScore()
    {
        if(_totalScore > _highScore)
            PlayerPrefs.SetInt("hs",_totalScore);
    }

    public void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt("hs", 0);
        OnHighScoreChanged?.Invoke(_highScore);
    }

    private void Start()
    {
        LoadHighScore();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += SaveHighScore;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= SaveHighScore;
    }
}
