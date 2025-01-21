using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class DoorActivateOnConnect : MonoBehaviour
{
    public int playerCount = 0;
    public Rigidbody doorRigidbody;
    public XRGrabInteractable xrGrabInteractable;

    void Start()
    {
        LockDoor();
    }

    public void OnPlayerConnected() // Call when a player connects
    {
        playerCount++;
        CheckPlayerCount();
    }

    public void OnPlayerDisconnected() // Call when a player disconnects
    {
        playerCount--;
        CheckPlayerCount();
    }

    private void CheckPlayerCount()
    {
        if (playerCount == 2)
        {
            UnlockDoor();
        }
        else
        {
            LockDoor();
        }
    }

    // Lock the door
    public void LockDoor()
    {
        if (doorRigidbody != null)
            doorRigidbody.isKinematic = true; // Disable physics interactions

        if (xrGrabInteractable != null)
            xrGrabInteractable.enabled = false; // Disable grabbing
    }

    // Unlock the door
    public void UnlockDoor()
    {
        if (doorRigidbody != null)
            doorRigidbody.isKinematic = false; // Re-enable physics interactions

        if (xrGrabInteractable != null)
            xrGrabInteractable.enabled = true; // Re-enable grabbing
    }
}
