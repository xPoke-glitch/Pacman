using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : Singleton<ScoreManager>
{
    public static event Action<int> OnScoreChanged;

    private int _totalScore = 0;

    public void AddPoint(int amount)
    {
        _totalScore += amount;
        OnScoreChanged?.Invoke(_totalScore);
    }

    public void ResetScore()
    {
        // Saving high score ecc here ...
        _totalScore = 0;

        OnScoreChanged?.Invoke(_totalScore);
    }
}
