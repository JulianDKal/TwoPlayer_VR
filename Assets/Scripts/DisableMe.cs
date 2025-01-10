using System;
using Unity.Netcode;
using UnityEngine;

public class DisableMe : NetworkBehaviour
{
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            if (NetworkManager.Singleton.LocalClientId == 0)
            {
                mr.enabled = false;
            }
        }
    }
}
