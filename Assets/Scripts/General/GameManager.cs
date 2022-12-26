using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnGameOver;

    public void GameOver()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Game Over");
        OnGameOver?.Invoke();
    }

    public void LoadGame() => SceneManager.LoadScene("Game");

    public void QuitGame() => Application.Quit();
}
