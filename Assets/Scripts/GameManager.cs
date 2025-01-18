using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public int puzzleOneScore = 0;
    public NetworkVariable<int> PuzzlesSolved = new NetworkVariable<int>();
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsHost)
        {
            // Initialize values only on the server
            //PuzzleOneScore.Value = 0;
            PuzzlesSolved.Value = 0;
        }
    }

    private void Start()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);

        PuzzlesSolved.OnValueChanged += (oldValue, newValue) =>
        {
            if (newValue >= 1)
            {
                EventManager.instance.allPuzzlesCompleted();
            }
        };
    }
    
    // Method to update scores
    public void UpdatePuzzlesSolved()
    {
        if (IsServer)
        {
            // Only the server updates the NetworkVariable
            PuzzlesSolved.Value++;
        }
    }
}
