using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Ghost ghost;

    private void Update()
    {
        animator.SetInteger("X", (int)ghost.MovementDirection.x);
        animator.SetInteger("Y", (int)ghost.MovementDirection.y);

        if (ghost.StateMachine.CurrentState is FrightenedState)
            animator.SetBool("IsVulnerable", true);
        else
            animator.SetBool("IsVulnerable", false);
        if (ghost.StateMachine.CurrentState is EatenState)
            animator.SetBool("IsEaten", true);
        else
            animator.SetBool("IsEaten", false);
    }
}
