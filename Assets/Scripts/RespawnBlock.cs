using System;
using UnityEngine;

public class RespawnBlock : MonoBehaviour
{
    public Transform respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Respawn block entered");
        if (other.CompareTag("Player"))
        {
            //set x and z position to respawnPos, but leave y alone
            Debug.Log("Player entered respawn block");
            other.transform.position = new Vector3(respawnPoint.position.x, other.transform.position.y, respawnPoint.position.z);
        }
    }
}
