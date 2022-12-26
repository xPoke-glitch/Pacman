using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BigPoint : Point
{
    public static event Action OnBigPointCollected;

    public override void Collect()
    {
        // Trigger Escape state ghosts HERE...
        OnBigPointCollected?.Invoke();

        // After
        base.Collect();
    }
}
