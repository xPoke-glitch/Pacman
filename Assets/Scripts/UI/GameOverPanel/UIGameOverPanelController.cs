using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverPanelController : MonoBehaviour
{
    [Header("View References")]
    [SerializeField]
    private UIGameOverPanel panel;

    private void ShowPanel() => panel.ActivePanel(true);
    private void HidePanel() => panel.ActivePanel(false);

    private void Start() => HidePanel();

    private void OnEnable()
    {
        GameManager.OnGameOver += ShowPanel;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowPanel;
    }
}
