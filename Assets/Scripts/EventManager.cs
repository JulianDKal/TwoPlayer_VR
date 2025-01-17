using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event Action puzzleOneCompletedEvent;
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    public void puzzleOneCompleted()
    {
        puzzleOneCompletedEvent?.Invoke();
        Debug.Log("puzzle one completed!");
        puzzleOneCompletedEvent = null;
    }
}
