using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : Singleton<PointManager>
{
    private int _pointCount = 0;
    public void IncrementPointCount() => _pointCount++;
    public void DecrementPointCount()
    {
        _pointCount--;
        // Win condition
        if (_pointCount <= 0)
            GameManager.Instance.GameOver(); 
    }
}
