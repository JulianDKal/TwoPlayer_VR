using UnityEngine;
using UnityEngine.Networking;

public class RespawnPositionChanger : MonoBehaviour
{
    public Transform startPos;
    public Transform newPos;
    public GameObject respawnPoint;
    private int playerCount = 0;
    
    void Start()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn object not set!");
        }
    }

    public void OnPlayerConnected()
    {
        playerCount++;
        UpdateRespawnPoint();
    }

    public void OnPlayerDisconnected()
    {
        playerCount--;
        UpdateRespawnPoint();
    }

    private void UpdateRespawnPoint()
    {
        if (playerCount == 2)
        {
            respawnPoint.transform.position = newPos.position;
            respawnPoint.transform.rotation = newPos.rotation;
        }
        else
        {
            respawnPoint.transform.position = startPos.position;
            respawnPoint.transform.rotation = startPos.rotation;
        }
    }
}