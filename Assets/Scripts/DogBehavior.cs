using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DogBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;   // Dog's NavMeshAgent component
    public Animator animator;           // Dog's Animator component
    public Transform[] targets;         // List of target positions for the sequence
    public Transform player;            // The player that the dog follows

    [Header("Settings")]
    public float playerProximity = 2f;      // Distance considered “close enough” to the player
    public float followDuration = 5f;       // How long to follow the player during the sequence
    public float idleWaitTime = 2f;         // Pause between moving to each target
    public float jumpAnimationLength = 0.8f;  // Duration of the jump animation

    private int currentTargetIndex = 0;
    private bool sequenceStarted = false;
    private Coroutine behaviorRoutine;

    public SwitchController switchController; // Reference to the SwitchController

    void Start()
    {
        // Reset the switch flag so the dog starts in follow-player mode
        if (switchController != null)
        {
            switchController.flagFireOn = false;
        }
        sequenceStarted = false;
        navMeshAgent.isStopped = false;
        // Start following the player continuously
        behaviorRoutine = StartCoroutine(FollowPlayer());
        Debug.Log("Starting the dog to follow player");
    }

    void OnEnable()
    {
        // Optionally ensure that when the GameObject becomes active,
        // we restart following the player if no sequence is running.
        if (!sequenceStarted)
        {
            if (behaviorRoutine != null)
            {
                StopCoroutine(behaviorRoutine);
            }
            behaviorRoutine = StartCoroutine(FollowPlayer());
            Debug.Log("restart following the player if no sequence is running");
        }
    }

    void Update()
    {
        // If the switch has been activated, start the behavior sequence.
        if (!sequenceStarted && switchController != null && switchController.flagFireOn)
        {
            sequenceStarted = true;
            if (behaviorRoutine != null)
            {
                StopCoroutine(behaviorRoutine);
            }
            behaviorRoutine = StartCoroutine(DogBehaviorSequence());
        }
    }

    // Sequence: move to a target, then follow the player briefly, then repeat.
    IEnumerator DogBehaviorSequence()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            yield return StartCoroutine(MoveToTarget());
            // Then follow the player until close or for a fixed duration.
            yield return StartCoroutine(FollowPlayerSequence());
            // Wait for a moment before moving to the next target.
            yield return new WaitForSeconds(idleWaitTime);
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
        // After the sequence, ensure the dog stops moving.
        SetWalking(false);
        navMeshAgent.isStopped = true;
    }

    // Move toward the current target, pausing if the player is too far away.
    IEnumerator MoveToTarget()
    {
        SetWalking(true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(targets[currentTargetIndex].position);

        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.2f)
        {
            // If the player is too far away, pause and wag tail until the player is back close.
            if (IsPlayerTooFar())
            {
                SetWalking(false);
                SetTailWag(true);
                navMeshAgent.isStopped = true;

                yield return new WaitUntil(() => !IsPlayerTooFar());

                // Resume moving to the target.
                SetTailWag(false);
                SetWalking(true);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targets[currentTargetIndex].position);
            }
            yield return null;
        }

        // When the target is reached, stop walking and trigger a jump animation.
        SetWalking(false);
        TriggerJump();
        yield return new WaitForSeconds(jumpAnimationLength);
    }

    // Follow the player for a fixed duration or until close enough.
    IEnumerator FollowPlayerSequence()
    {
        SetTailWag(false);
        SetWalking(true);
        navMeshAgent.isStopped = false;
        float followTimer = 0f;

        while (followTimer < followDuration)
        {
            navMeshAgent.SetDestination(player.position);

            // If the dog is already close enough to the player, break out.
            if (!IsPlayerTooFar())
            {
                break;
            }
            followTimer += Time.deltaTime;
            yield return null;
        }
        SetWalking(false);
    }

    // Continuous follow behavior until the switch is activated.
    IEnumerator FollowPlayer()
    {
        while (switchController != null && !switchController.flagFireOn)
        {
            if (IsPlayerTooFar())
            {
                SetWalking(true);
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                SetWalking(false);
            }
            yield return null;
        }
    }

    // Returns true if the dog is farther than the allowed proximity from the player.
    bool IsPlayerTooFar() =>
        Vector3.Distance(transform.position, player.position) > playerProximity;

    // Trigger jump animation.
    void TriggerJump()
    {
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Jump");
    }

    // Set walking animation state.
    void SetWalking(bool state)
    {
        animator.SetBool("isWalking", state);
    }

    // Set tail wag animation state.
    void SetTailWag(bool state)
    {
        animator.SetBool("TailWag", state);
    }

    void OnDisable()
    {
        if (behaviorRoutine != null)
        {
            StopCoroutine(behaviorRoutine);
        }
    }
}
