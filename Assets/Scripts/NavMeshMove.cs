using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Transform[] targets;
    public Transform player;
    public float waitTime = 3f; // Dog waits after reaching target
    public float followDuration = 5f; // Time dog follows player
    public float playerProximity = 2f; // Distance player needs to be near for interaction

    private int currentTargetIndex = 0;
    private bool isFollowingPlayer = false;

    void Start()
    {
        if (targets.Length > 0)
        {
            StartCoroutine(DogRoutine());
        }
    }

    IEnumerator DogRoutine()
    {
        // Initial tail wag
        animator.SetTrigger("TailWag");
        yield return new WaitForSeconds(2f);

        while (true)
        {
            // Jump to indicate movement
            animator.SetTrigger("Jump");
            yield return new WaitForSeconds(1f);

            // Move to target
            navMeshAgent.SetDestination(targets[currentTargetIndex].position);
            animator.SetBool("isWalking", true);

            // Wait until the dog reaches the target
            while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            // Stop walking animation
            animator.SetBool("isWalking", false);

            // Wait at target before checking for player
            yield return new WaitForSeconds(waitTime);

            // Wait for player to approach
            while (Vector3.Distance(transform.position, player.position) > playerProximity)
            {
                animator.SetBool("isIdle", true);
                yield return null;
            }

            // Player arrived - react
            animator.SetBool("isIdle", false);
            animator.SetTrigger("Jump");
            animator.SetTrigger("TailWag");
            yield return new WaitForSeconds(2f);

            // Follow player for a set duration
            isFollowingPlayer = true;
            StartCoroutine(FollowPlayer());

            yield return new WaitForSeconds(followDuration);
            isFollowingPlayer = false;

            // Wait before moving to the next target
            yield return new WaitForSeconds(waitTime);

            // Move to the next target
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }

    IEnumerator FollowPlayer()
    {
        while (isFollowingPlayer)
        {
            navMeshAgent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            yield return null;
        }
        animator.SetBool("isWalking", false);
    }
}
