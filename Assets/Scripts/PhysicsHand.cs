using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsHand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform trackedTransform; // Parent with TrackedPoseDriver
    [SerializeField] private Rigidbody handRigidbody;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float maxDistance = 0.5f; // Max distance hand can drift from controller

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    
    private void Awake()
    {
        // Configure the rigidbody
        handRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        handRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        handRigidbody.mass = 20f; // Increased mass helps prevent clipping
        handRigidbody.maxAngularVelocity = 20f;
        
        // Initialize position
        targetPosition = trackedTransform.position;
        targetRotation = trackedTransform.rotation;
    }

    private void FixedUpdate()
    {
        targetPosition = trackedTransform.position;
        targetRotation = trackedTransform.rotation;

        // Move the physics hand to the controller position
        Vector3 positionDelta = targetPosition - handRigidbody.position;
        float distance = positionDelta.magnitude;

        // If hand has drifted too far, teleport it back
        if (distance > maxDistance)
        {
            handRigidbody.position = targetPosition - (positionDelta.normalized * maxDistance);
            handRigidbody.linearVelocity = Vector3.zero;
        }
        
        // Apply velocity to move towards target
        Vector3 velocity = positionDelta * followSpeed;
        handRigidbody.linearVelocity = Vector3.ClampMagnitude(velocity, followSpeed);

        // Handle rotation
        Quaternion rotationDelta = targetRotation * Quaternion.Inverse(handRigidbody.rotation);
        Vector3 rotationAxis;
        float rotationAngle;
        rotationDelta.ToAngleAxis(out rotationAngle, out rotationAxis);

        // Normalize angle
        if (rotationAngle > 180)
            rotationAngle -= 360;

        // Apply angular velocity to rotate towards target
        if (Mathf.Abs(rotationAngle) > 0)
        {
            rotationAxis.Normalize();
            handRigidbody.angularVelocity = rotationAxis * (rotationAngle * Mathf.Deg2Rad * rotateSpeed);
        }
    }
}