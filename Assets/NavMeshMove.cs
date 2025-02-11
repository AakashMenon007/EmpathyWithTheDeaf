using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform[] targets; // Assign target transforms in the Inspector
    public float playerFollowDuration = 5f; // Duration to follow the player
    public float waitForPlayerDistance = 2f; // Distance to wait for the player

    private int currentTargetIndex = 0;
    private Transform player;
    private bool isFollowingPlayer = false;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DogBehaviorSequence());
    }

    IEnumerator DogBehaviorSequence()
    {
        while (true)
        {
            // Play jump and tail wagging animations
            animator.SetTrigger("Jump");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetTrigger("TailWag");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Move to the current target
            agent.SetDestination(targets[currentTargetIndex].position);
            animator.SetBool("isWalking", true);

            // Wait until the dog reaches the target
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Stop walking animation
            animator.SetBool("isWalking", false);

            // Play jump and tail wagging animations upon arrival
            animator.SetTrigger("Jump");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.SetTrigger("TailWag");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Wait for the player to come near
            while (Vector3.Distance(transform.position, player.position) > waitForPlayerDistance)
            {
                yield return null;
            }

            // Follow the player for a set duration
            isFollowingPlayer = true;
            StartCoroutine(FollowPlayer());

            // Wait for the follow duration to complete
            yield return new WaitForSeconds(playerFollowDuration);

            isFollowingPlayer = false;

            // Move to the next target
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }

    IEnumerator FollowPlayer()
    {
        animator.SetBool("isWalking", true);
        while (isFollowingPlayer)
        {
            agent.SetDestination(player.position);
            yield return null;
        }
        animator.SetBool("isWalking", false);
    }
}
