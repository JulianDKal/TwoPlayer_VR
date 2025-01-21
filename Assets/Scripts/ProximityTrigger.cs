using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    private PhysicsHandJoint parentScript;

    private void Start()
    {
        parentScript = GetComponentInParent<PhysicsHandJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandCollision"))
        {
            parentScript.EnablePhysics();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandCollision"))
        {
            parentScript.DisablePhysics();
        }
    }
}