using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event Action puzzleOneCompletedEvent;
    public event Action allPuzzlesCompletedEvent;
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

    public void allPuzzlesCompleted()
    {
        Debug.Log("all puzzles completed!");
        if(allPuzzlesCompletedEvent == null) Debug.Log("all puzzles completed event already empty");
        allPuzzlesCompletedEvent?.Invoke();
    }
}
