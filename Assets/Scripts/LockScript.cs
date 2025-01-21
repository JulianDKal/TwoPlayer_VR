using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockScript : NetworkBehaviour
{
    public GameObject chain;
    public GameObject chain2;
    
    Rigidbody rb1;
    Rigidbody rb2;

    private void Awake()
    {
        rb1 = chain.GetComponent<Rigidbody>();
        rb2 = chain2.GetComponent<Rigidbody>();
    }

    public void KeyInserted(SelectEnterEventArgs args)
    {
        Debug.Log("Key inserted");

        //chain.SetActive(false);
        //chain2.SetActive(false);

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
            DisappearLockClientRpc();
            DisappearLock();
        }
        else
        {
            rb1.isKinematic = false;
            rb2.isKinematic = false;
            KeyInsertedServerRpc();
            //DisappearLock();
        }
        
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

    [ClientRpc]
    private void DisappearLockClientRpc()
    {
        Debug.Log("key inserted on client side");
        rb1.isKinematic = false;
        rb2.isKinematic = false;
    }

    private void DisappearLock()
    {
        rb1.isKinematic = false;
        rb2.isKinematic = false;
        NetworkObject networkObject = gameObject.GetComponent<NetworkObject>();
        NetworkObject chainNetworkObject = chain.GetComponent<NetworkObject>();
        NetworkObject chain2NetworkObject = chain2.GetComponent<NetworkObject>();
        
        if (networkObject != null)
        {
            networkObject.Despawn(true);
        }
        // if (chainNetworkObject != null) chainNetworkObject.Despawn(true);
        // if (chain2NetworkObject != null) chain2NetworkObject.Despawn(true);
        
        this.gameObject.SetActive(false);
    }
}
