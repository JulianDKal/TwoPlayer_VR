using UnityEngine;

public class CustomHinge : MonoBehaviour
{
    [Header("Axis and Limits")]
    public Vector3 rotationAxis = Vector3.up; // The axis to limit rotation (e.g., Vector3.up for Y-axis)
    public float minAngle = -45f;
    public float maxAngle = 45f;

    private Quaternion initialRotation;
    private Vector3 worldAxis;

    void Start()
    {
        // Store the initial rotation and calculate the world rotation axis
        initialRotation = transform.rotation;
        worldAxis = transform.TransformDirection(rotationAxis.normalized);
    }

    void Update()
    {
        // Calculate the current rotation relative to the initial rotation
        Quaternion currentRotation = transform.rotation;
        Quaternion relativeRotation = Quaternion.Inverse(initialRotation) * currentRotation;

        // Extract the angle around the desired axis
        float angle;
        Vector3 axis;
        relativeRotation.ToAngleAxis(out angle, out axis);

        // Ensure the angle is consistent with the chosen axis
        angle *= Mathf.Sign(Vector3.Dot(axis, worldAxis));

        // Clamp the angle
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Apply the limited rotation
        transform.rotation = initialRotation * Quaternion.AngleAxis(clampedAngle, worldAxis);
    }
}
