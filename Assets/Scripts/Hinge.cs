using UnityEngine;

public class Hinge : MonoBehaviour
{
    private Quaternion startLocalRotation;
    private Transform originalParent;

    // Separate limits for positive and negative rotation for each axis
    public Vector3 positiveRotationLimits; // Maximum positive rotation offset
    public Vector3 negativeRotationLimits; // Maximum negative rotation offset

    void Start()
    {
        startLocalRotation = transform.localRotation;
        originalParent = transform.parent;
    }

    void Update()
    {
        // Restore original parent if changed
        if (transform.parent != originalParent)
        {
            transform.SetParent(originalParent, true);
        }

        // Get current local euler angles
        Vector3 currentLocalEuler = transform.localEulerAngles;

        // Normalize euler angles to handle angle wrapping
        Vector3 normalizedCurrentEuler = new Vector3(
            NormalizeAngle(currentLocalEuler.x),
            NormalizeAngle(currentLocalEuler.y),
            NormalizeAngle(currentLocalEuler.z)
        );

        // Calculate clamped rotation based on positive and negative limits
        Vector3 clampedLocalEuler = new Vector3(
            Mathf.Clamp(normalizedCurrentEuler.x,
                NormalizeAngle(startLocalRotation.eulerAngles.x + negativeRotationLimits.x),
                NormalizeAngle(startLocalRotation.eulerAngles.x + positiveRotationLimits.x)),
            Mathf.Clamp(normalizedCurrentEuler.y,
                NormalizeAngle(startLocalRotation.eulerAngles.y + negativeRotationLimits.y),
                NormalizeAngle(startLocalRotation.eulerAngles.y + positiveRotationLimits.y)),
            Mathf.Clamp(normalizedCurrentEuler.z,
                NormalizeAngle(startLocalRotation.eulerAngles.z + negativeRotationLimits.z),
                NormalizeAngle(startLocalRotation.eulerAngles.z + positiveRotationLimits.z))
        );

        // Set local rotation
        transform.localEulerAngles = clampedLocalEuler;
    }

    // Helper method to normalize angles between -180 and 180 degrees
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180f) angle -= 360f;
        if (angle < -180f) angle += 360f;
        return angle;
    }
}
