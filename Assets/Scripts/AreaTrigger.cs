using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public GameObject objectToActivate; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the Player enters
        {
            objectToActivate.SetActive(true); // Activate the object
        }
    }
}