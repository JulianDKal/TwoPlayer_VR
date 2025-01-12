using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DebugMe : NetworkBehaviour
{
    private NetworkObject _networkObject;
    private XRBaseInteractable _interactable;
    private Rigidbody _rigidbody;
    void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _networkObject = GetComponent<NetworkObject>();
        if (_networkObject == null)
        {
            Debug.LogWarning("No network object attached to " + gameObject.name);
        }
        
        _interactable.selectEntered.AddListener(OnSelectEnter); //change ownership when instance interacts with this object
        _interactable.selectExited.AddListener(OnSelectExit);
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkDespawn();
        Debug.Log($"I ({this.gameObject.name}) am owned by this instance: {IsOwner}, OwnerClientId is: {_networkObject.OwnerClientId}, and is owned by the server: {_networkObject.IsOwnedByServer}");
        
    }
    
    public override void OnGainedOwnership()
    {
        base.OnGainedOwnership();
        Debug.Log($"I am now the owner of this object: {NetworkManager.Singleton.LocalClientId}");
    }

    public override void OnLostOwnership()
    {
        base.OnLostOwnership();
        Debug.Log($"I lost ownership: {NetworkManager.Singleton.LocalClientId}");
    }

    private void OnSelectEnter(SelectEnterEventArgs eventArgs)
    {
        Debug.Log("Owner is: " + _networkObject.OwnerClientId + ", " + _networkObject.IsOwnedByServer);
    }

    private void OnSelectExit(SelectExitEventArgs eventArgs)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }
    
}
