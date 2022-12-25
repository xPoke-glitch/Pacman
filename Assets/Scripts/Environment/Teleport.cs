using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleport : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform teleportPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = teleportPoint.position;
    }
}
