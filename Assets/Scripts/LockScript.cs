using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockScript : NetworkBehaviour
{
    public GameObject chain;
    public GameObject chain2;
    
    public void KeyInserted(SelectEnterEventArgs args)
    {
        Debug.Log("Key inserted");
        chain.SetActive(false);
        chain2.SetActive(false);

        if (IsServer)
        {
            GameManager.instance.UpdatePuzzlesSolved();
        }
        else SubmitPuzzleScoreServerRpc();
        
        // if (GameManager.instance.PuzzlesSolved.Value >= 1)
        // {
        //     EventManager.instance.allPuzzlesCompleted();
        // }
        args.interactableObject.transform.gameObject.SetActive(false); //set key to inactive
        
        if (IsServer)
        {
            DisappearLock();
        }
        else KeyInsertedServerRpc();
        
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void SubmitPuzzleScoreServerRpc()
    {
        GameManager.instance.UpdatePuzzlesSolved();
    }

    [ServerRpc(RequireOwnership = false)]
    private void KeyInsertedServerRpc()
    {
        DisappearLock();
    }

    private void DisappearLock()
    {
        NetworkObject networkObject = gameObject.GetComponent<NetworkObject>();
        NetworkObject chainNetworkObject = chain.GetComponent<NetworkObject>();
        NetworkObject chain2NetworkObject = chain2.GetComponent<NetworkObject>();
        
        if (networkObject != null)
        {
            networkObject.Despawn(true);
        }
        if (chainNetworkObject != null) chainNetworkObject.Despawn(true);
        if (chain2NetworkObject != null) chain2NetworkObject.Despawn(true);
        this.gameObject.SetActive(false);
    }
}
