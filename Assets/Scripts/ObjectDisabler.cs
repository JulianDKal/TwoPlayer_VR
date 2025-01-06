using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ObjectDisabler : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log("ObjectDisabler Awake");
    }

    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        Debug.Log("||||||||||| ObjectDisabler OnNetworkSpawned for " + NetworkManager.Singleton.LocalClientId);
        if(NetworkManager.Singleton.LocalClientId == 0) DisableObjectsFromPlayer2();
        else DisableObjectsFromPlayer1();
        base.OnNetworkSpawn();
    }

    private void DisableObjectsFromPlayer2()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);
        foreach (var obj in rootObjects)
        {
            if (obj.CompareTag("Player2"))
            {
                obj.SetActive(false);
            }
        }
    }
    private void DisableObjectsFromPlayer1()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);
        foreach (var obj in rootObjects)
        {
            if (obj.CompareTag("Player1"))
            {
                obj.SetActive(false);
            }
        }
    }
}
