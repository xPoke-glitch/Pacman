using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image panel;
    [SerializeField]
    private GameObject buttons;

    public void ActivePanel(bool active)
    {
        panel.enabled = active;
        buttons.SetActive(active);
    }
}
