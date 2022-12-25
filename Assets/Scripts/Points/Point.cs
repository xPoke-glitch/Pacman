using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Point : MonoBehaviour
{
    [Header("Point Settings")]
    [SerializeField]
    private int amount = 100;

    public virtual void Collect()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Point Collected with amount " + amount);
        ScoreManager.Instance.AddPoint(amount);
        Destroy(gameObject);
    }
}
