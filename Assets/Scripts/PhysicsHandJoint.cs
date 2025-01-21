using UnityEngine;

public class PhysicsHandJoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform controllerTransform;

    [Header("Physics Settings")]
    [SerializeField] private float positionForce = 5000f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] private float maxVelocity = 5f;
    [SerializeField] private float maxAngularVelocity = 10f;
    [SerializeField] private bool hideController = true;

    private Rigidbody rb;
    private MeshRenderer controllerRenderer;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private void Start()
    {
        // Setup rigidbody
        rb = GetComponent<Rigidbody>();
        rb.mass = 1f;
        rb.linearDamping = 5f;
        rb.angularDamping = 5f;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.maxAngularVelocity = maxAngularVelocity;
        rb.maxDepenetrationVelocity = 2f;

        // Optionally hide the controller visual
        if (hideController && controllerTransform != null)
        {
            controllerRenderer = controllerTransform.GetComponentInChildren<MeshRenderer>();
            if (controllerRenderer != null)
                controllerRenderer.enabled = false;
        }

        // Initialize position and rotation
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        // Calculate target velocities
        Vector3 positionDelta = (controllerTransform.position - rb.position);
        Vector3 targetVelocity = positionDelta * positionForce * Time.fixedDeltaTime;
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, maxVelocity);

        // Apply position
        rb.linearVelocity = targetVelocity;

        // Handle rotation
        Quaternion rotationDelta = controllerTransform.rotation * Quaternion.Inverse(rb.rotation);
        rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);

        // Normalize angle
        if (angle > 180f)
            angle -= 360f;

        if (Mathf.Abs(angle) > 0f)
        {
            Vector3 targetAngularVelocity = axis.normalized * angle * rotationForce * Time.fixedDeltaTime;
            targetAngularVelocity = Vector3.ClampMagnitude(targetAngularVelocity, maxAngularVelocity);
            rb.angularVelocity = targetAngularVelocity;
        }

        // Store previous values
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Optional: Add haptic feedback here if desired
    }

    private void OnDrawGizmos()
    {
        // Visual debug
        if (controllerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, controllerTransform.position);
        }
    }
}