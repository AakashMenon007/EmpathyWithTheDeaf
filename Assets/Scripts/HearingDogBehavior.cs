using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

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

    public Coroutine behaviorRoutine;
    public bool sequenceStarted = false;

    public SwitchController switchController;

    void Start()
    {
        // Initially, have the dog follow the player.
        if (player != null)
        {
            behaviorRoutine = StartCoroutine(FollowPlayer());
        }
    }

    void Update()
    {
        // When the switch is activated, change the dog's behavior.
        if (switchController != null && switchController.flagFireOn && !sequenceStarted)
        {
            sequenceStarted = true;
            if (behaviorRoutine != null)
            {
                StopCoroutine(behaviorRoutine);
            }
            behaviorRoutine = StartCoroutine(DogBehaviorSequence());
        }
    }

    IEnumerator DogBehaviorSequence()
    {
        // Loop through each target in the list.
        for (int i = 0; i < targets.Length; i++)
        {
            // Move to target.
            yield return StartCoroutine(MoveToTarget());

            // Then follow the player until close (or for a fixed duration).
            yield return StartCoroutine(FollowPlayer());

            // Wait for a moment before moving to the next target.
            yield return new WaitForSeconds(idleWaitTime);

            // Prepare for the next target.
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
        // Once finished, ensure the dog stops moving.
        SetWalking(false);
    }

    IEnumerator MoveToTarget()
    {
        SetWalking(true);
        navMeshAgent.SetDestination(targets[currentTargetIndex].position);

        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (IsPlayerTooFar())
            {
                // Pause movement and wait for player.
                SetWalking(false);
                SetTailWag(true);
                navMeshAgent.isStopped = true;

                yield return new WaitUntil(() => !IsPlayerTooFar());

                // Resume movement.
                SetTailWag(false);
                SetWalking(true);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targets[currentTargetIndex].position);
            }
            yield return null;
        }

        // Target reached – trigger jump animation.
        SetWalking(false);
        TriggerJump();
        yield return new WaitForSeconds(jumpAnimationLength);
    }

    IEnumerator FollowPlayer()
    {
        SetTailWag(false);
        SetWalking(true);
        float followTimer = 0f;

        while (followTimer < followDuration)
        {
            navMeshAgent.SetDestination(player.position);

            // If the dog is already close enough, break out to avoid circling.
            if (!IsPlayerTooFar())
            {
                break;
            }

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