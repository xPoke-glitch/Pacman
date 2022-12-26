using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System;

public class Gate : MonoBehaviour
{
    public static event Action OnFirstClose;
    [Header("References")]
    [SerializeField]
    private BoxCollider2D gateCollider;

    public bool IsOpen { get; private set; }

    private int _ghostsEatenCount = 0;

    public void Open()
    {
        if (_ghostsEatenCount <= 0)
            return;

        if (IsOpen)
            return;

        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GATE OPEN");
        IsOpen = true;
        gateCollider.enabled = false;
    }

    public void Close()
    {
        if (_ghostsEatenCount > 0)
            return;

        if(!IsOpen)
            return;

        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GATE CLOSED");
        IsOpen = false;
        gateCollider.enabled = true;
    }

    private void Start()
    {
        IsOpen = true;
        gateCollider.enabled = false;
    }

    private void OnEnable()
    {
        EatenState.OnGhostEaten += GhostEaten;
        EatenState.OnGhostRestored += GhostRestored;
    }

    private void OnDisable()
    {
        EatenState.OnGhostEaten -= GhostEaten;
        EatenState.OnGhostRestored -= GhostRestored;
    }

    private void GhostEaten()
    {
        _ghostsEatenCount++;
        Open();
    }

    private void GhostRestored()
    {
        _ghostsEatenCount--;
        if (_ghostsEatenCount < 0)
            _ghostsEatenCount = 0;
        Close();
    }

    private IEnumerator COWaitForFirstClose()
    {
        yield return new WaitForSeconds(3.0f);
        IsOpen = false;
        gateCollider.enabled = true;
        OnFirstClose?.Invoke();
    }
}
