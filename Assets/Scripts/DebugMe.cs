using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DebugMe : NetworkBehaviour
{
    private NetworkObject _networkObject;
    private XRBaseInteractable _interactable;
    void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _networkObject = GetComponent<NetworkObject>();
        if (_networkObject == null)
        {
            Debug.LogWarning("No network object attached to " + gameObject.name);
        }
        
        //_interactable.selectEntered.AddListener(OnSelectEnter); //change ownership when instance interacts with this object
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkDespawn();
        Debug.Log($"I ({this.gameObject.name}) am owned by this instance: {IsOwner}, OwnerClientId is: {_networkObject.OwnerClientId}, and is owned by the server: {_networkObject.IsOwnedByServer}");
        
    }

    private void OnSelectEnter(SelectEnterEventArgs eventArgs)
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId; // Replace with the correct client ID
        if (!_networkObject.IsOwner)
        {
            //_networkObject.OwnerClientId = clientId;
        }
    }
    
}
