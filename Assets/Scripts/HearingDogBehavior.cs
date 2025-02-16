using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HearingDogBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Transform[] targets;
    public Transform player;

    [Header("Settings")]
    public float playerProximity = 2f;
    public float followDuration = 5f;
    public float idleWaitTime = 2f;
    public float jumpAnimationLength = 1f;

    private int currentTargetIndex = 0;
    private bool isFollowingPlayer;
    private Coroutine behaviorRoutine;

    void Start()
    {
        if (targets.Length > 0 && player != null)
        {
            behaviorRoutine = StartCoroutine(DogBehaviorSequence());
        }
        else
        {
            Debug.LogError("Missing targets or player reference!");
        }
    }

    IEnumerator DogBehaviorSequence()
    {
        // Initial tail wag
        SetTailWag(true);
        yield return new WaitForSeconds(2f);

        while (true)
        {
            // Move to current target
            yield return StartCoroutine(MoveToTarget());

            // Follow player sequence
            yield return StartCoroutine(FollowPlayer());

            // Prepare for next target
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            yield return new WaitForSeconds(idleWaitTime);
        }
    }

    IEnumerator MoveToTarget()
    {
        // Start moving to target
        TriggerJump();
        yield return new WaitForSeconds(jumpAnimationLength / 2);

        SetWalking(true);
        navMeshAgent.SetDestination(targets[currentTargetIndex].position);

        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (IsPlayerTooFar())
            {
                // Pause movement and wait for player
                SetWalking(false);
                SetTailWag(true);
                navMeshAgent.isStopped = true;

                yield return new WaitUntil(() => !IsPlayerTooFar());

                // Resume movement
                SetTailWag(false);
                TriggerJump();
                yield return new WaitForSeconds(jumpAnimationLength / 2);
                SetWalking(true);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targets[currentTargetIndex].position);
            }
            yield return null;
        }

        // Target reached
        SetWalking(false);
        TriggerJump();
        yield return new WaitForSeconds(jumpAnimationLength);
    }

    IEnumerator FollowPlayer()
    {
        SetTailWag(false);
        TriggerJump();
        yield return new WaitForSeconds(jumpAnimationLength / 2);

        SetWalking(true);
        float followTimer = 0f;

        while (followTimer < followDuration)
        {
            if (IsPlayerTooFar())
            {
                // Interrupt following if player gets too far
                break;
            }

            navMeshAgent.SetDestination(player.position);
            followTimer += Time.deltaTime;
            yield return null;
        }

        SetWalking(false);
    }

    // Helper methods
    bool IsPlayerTooFar() =>
        Vector3.Distance(transform.position, player.position) > playerProximity;

    void TriggerJump()
    {
        animator.ResetTrigger("Jump"); // Clear previous triggers
        animator.SetTrigger("Jump");
    }

    void SetWalking(bool state) =>
        animator.SetBool("isWalking", state);

    void SetTailWag(bool state) =>
        animator.SetBool("TailWag", state);

    void OnDisable()
    {
        if (behaviorRoutine != null)
        {
            StopCoroutine(behaviorRoutine);
        }
    }
}