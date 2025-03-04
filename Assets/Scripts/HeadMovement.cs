using UnityEngine;

public class SimpleHeadLook : MonoBehaviour
{
    public Transform player; // Assign the player's transform
    public Transform head; // Assign the head bone's transform
    public float rotationSpeed = 5f; // Speed of the head rotation

    void Update()
    {
        if (player != null && head != null)
        {
            // Calculate direction to look at the player
            Vector3 direction = player.position - head.position;
            direction.y = 0; // Keep the rotation horizontal

            // Smoothly rotate the head towards the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
