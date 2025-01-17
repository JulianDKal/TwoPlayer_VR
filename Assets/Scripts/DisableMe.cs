using System;
using Unity.Netcode;
using UnityEngine;

public class DisableMe : NetworkBehaviour
{
    private MeshRenderer mr;
    [Tooltip("This is the playerID for which this object is disabled. 0 for player 1 and 1 for player 2.")]
    public ulong playerID;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (NetworkManager.Singleton.LocalClientId == playerID)
        {
            mr.enabled = false;
        }
    }
}
