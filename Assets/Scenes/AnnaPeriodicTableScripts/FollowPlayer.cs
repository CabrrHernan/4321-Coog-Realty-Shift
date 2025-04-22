using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerHead; // XR Camera
    public float distance = 2.5f;
    public float heightOffset = -0.4f;
    public bool followRotation = true;

    void LateUpdate()
    {
        if (playerHead == null) return;

        // Get forward direction, flatten to horizontal plane
        Vector3 forward = playerHead.forward;
        forward.y = 0;
        forward.Normalize();

        // Position in front of player
        Vector3 targetPosition = playerHead.position + forward * distance + Vector3.up * heightOffset;
        transform.position = targetPosition;

        if (followRotation)
        {
            transform.LookAt(playerHead);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180f, 0); // Face the player
        }
    }
}
