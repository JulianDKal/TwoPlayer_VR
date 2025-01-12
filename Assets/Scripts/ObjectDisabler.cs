using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ObjectDisabler : NetworkBehaviour
{
    //These are the objects that belong to player 1 and need to be disabled on Player 2's side
    public List<GameObject> playerObjects1 = new List<GameObject>();
    //These are the objects that belong to player 2
    public List<GameObject> playerObjects2 = new List<GameObject>();
    void Awake()
    {
        Debug.Log("ObjectDisabler Awake");
    }

    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        Debug.Log("||||||||||| ObjectDisabler OnNetworkSpawned for Player" + (NetworkManager.Singleton.LocalClientId + 1));
        if(IsHost) Debug.Log("||||||||||||||||||| I am a host!");
        if(NetworkManager.Singleton.LocalClientId == 0) DisableObjectsFromPlayer2(); //disable all player 2 objects
        else DisableObjectsFromPlayer1();
        base.OnNetworkSpawn();
    }

    private void DisableObjectsFromPlayer2()
    {
        foreach (var obj in playerObjects2)
        {
            obj.SetActive(false);
        }
        // List<GameObject> rootObjects = new List<GameObject>();
        // SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);
        // foreach (var obj in rootObjects)
        // {
        //     if (obj.CompareTag("Player2"))
        //     {
        //         obj.SetActive(false);
        //     }
        // }
    }
    private void DisableObjectsFromPlayer1()
    {
        foreach (var obj in playerObjects1)
        {
            obj.SetActive(false);
        }
        // List<GameObject> rootObjects = new List<GameObject>();
        // SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);
        // foreach (var obj in rootObjects)
        // {
        //     if (obj.CompareTag("Player1"))
        //     {
        //         obj.SetActive(false);
        //     }
        // }
    }
}
