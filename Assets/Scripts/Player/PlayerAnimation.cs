using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerMovements movements;
    [SerializeField]
    private GameObject spriteObj;

    private void Update()
    {
        animator.SetBool("IsMoving", movements.IsMoving);

        spriteObj.transform.right = movements.MovementDirection * -1;
    }

}
