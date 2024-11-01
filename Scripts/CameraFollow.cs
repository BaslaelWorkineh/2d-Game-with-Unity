using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Boundary GameObjects
    public Transform leftBoundary;
    public Transform rightBoundary;
    public Transform topBoundary;
    public Transform bottomBoundary;

    void FixedUpdate()
    {
        // Calculate desired position with offset, ignore Z as it's fixed
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Set boundary limits based on GameObject positions
        float minX = leftBoundary.position.x;
        float maxX = rightBoundary.position.x;
        float minY = bottomBoundary.position.y;
        float maxY = topBoundary.position.y;

        // Clamp the desired position within the boundaries
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z); // Fixed Z position

        // Smoothly move the camera towards the clamped position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}

