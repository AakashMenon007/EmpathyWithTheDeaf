using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DogBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;   // Your dog's NavMeshAgent component
    public Animator animator;           // The dog's Animator component
    public Transform[] targets;         // List of target positions for the sequence
    public Transform player;            // The player the dog initially follows

    [Header("Settings")]
    public float playerProximity = 2f;      // How close the dog must be to consider itself "at" the player
    public float followDuration = 5f;       // How long the dog follows the player during the sequence
    public float idleWaitTime = 2f;         // Pause time between moving to each target
    public float jumpAnimationLength = 0.8f;  // How long the jump animation lasts

    private int currentTargetIndex = 0;
    private bool sequenceStarted = false;
    private Coroutine behaviorRoutine;

    public SwitchController switchController; // Reference to the SwitchController

    void Start()
    {
        // Start by following the player continuously until the switch is activated.
        behaviorRoutine = StartCoroutine(FollowPlayer());
    }

    void Update()
    {
        // Check if the switch has been activated.
        if (!sequenceStarted && switchController != null && switchController.flagFireOn)
        {
            sequenceStarted = true;

            // Stop the initial follow routine and start the sequence.
            if (behaviorRoutine != null)
            {
                StopCoroutine(behaviorRoutine);
            }
            behaviorRoutine = StartCoroutine(DogBehaviorSequence());
        }
    }

    // Sequence: Move to target, then follow player briefly, pause, and then move to next target.
    IEnumerator DogBehaviorSequence()
    {
        // Loop through each target in the list.
        for (int i = 0; i < targets.Length; i++)
        {
            // Move to the current target.
            yield return StartCoroutine(MoveToTarget());

            // Then follow the player until close or for a fixed duration.
            yield return StartCoroutine(FollowPlayerSequence());

            // Wait for a moment before moving to the next target.
            yield return new WaitForSeconds(idleWaitTime);

            // Cycle to the next target.
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
        // After the sequence, ensure the dog stops moving.
        SetWalking(false);
        navMeshAgent.isStopped = true;
    }

    // Moves the dog toward the current target.
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

    // The dog's behavior when following the player during the sequence.
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

    // Initial behavior: continuously follow the player until the switch is activated.
    IEnumerator FollowPlayer()
    {
        while (!switchController.flagFireOn)
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

    // Returns true if the dog is farther than playerProximity away from the player.
    bool IsPlayerTooFar() =>
        Vector3.Distance(transform.position, player.position) > playerProximity;

    // Triggers the jump animation.
    void TriggerJump()
    {
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Jump");
    }

    // Sets the walking animation.
    void SetWalking(bool state)
    {
        animator.SetBool("isWalking", state);
    }

    // Sets the tail wag animation.
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
