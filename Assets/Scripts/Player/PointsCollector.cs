using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Point point = null;
        if(collision.gameObject.TryGetComponent(out point))
        {
            point.Collect();
        }
    }
}
