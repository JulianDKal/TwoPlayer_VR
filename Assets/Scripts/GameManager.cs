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
    
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsHost)
        {
            PuzzlesSolved.Value = 0;
        }
    }

    private void Start()
    {
        PuzzlesSolved.OnValueChanged += (oldValue, newValue) =>
        {
            if (newValue >= 3)
            {
                EventManager.instance.allPuzzlesCompleted();
            }
        };
    }
    
    public void UpdatePuzzlesSolved()
    {
        Debug.Log("puzzle score");
        if (IsServer)
        {
            PuzzlesSolved.Value++;
        }
    }
}
