using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockScript : NetworkBehaviour
{
    public Rigidbody chain;
    public Rigidbody chain2;
    public GameObject key;
    public GameObject shaft;
    
    private Rigidbody rbLock;

    private void Awake()
    {
        rbLock = gameObject.GetComponent<Rigidbody>();
    }

    public void KeyInserted(SelectEnterEventArgs args)
    {
        Debug.Log("Key inserted");

        chain.isKinematic = false;
        chain2.isKinematic = false;

        rbLock.isKinematic = false;
        rbLock.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rbLock.interpolation = RigidbodyInterpolation.Interpolate;

        shaft.transform.position += new Vector3(0, 0.06f, 0);

        args.interactableObject.transform.gameObject.SetActive(false); //set key to inactive
        key.SetActive(true); //set fake key to active

        if (IsServer)
        {
            Debug.Log("is Server");
            GameManager.instance.UpdatePuzzlesSolved();
        }
        //else SubmitPuzzleScoreServerRpc();
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void SubmitPuzzleScoreServerRpc()
    {
        Debug.Log("is not Server");
        GameManager.instance.UpdatePuzzlesSolved();
    }
}