using System;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour
{
    private Rigidbody doorRigidbody;
    private BoxCollider handleCollider;
    public GameObject winScreen;
    
    private void Start()
    {
        EventManager.instance.allPuzzlesCompletedEvent += AllPuzzlesCompleted;
        
        doorRigidbody = GetComponent<Rigidbody>();
        handleCollider = GetComponentInChildren<BoxCollider>();
        
        doorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        handleCollider.enabled = false;
    }

    private void AllPuzzlesCompleted()
    {
        Debug.Log("All puzzles completed, final door is open!!");
        handleCollider.enabled = true;
        doorRigidbody.constraints = RigidbodyConstraints.None;
        winScreen.SetActive(true);
        EventManager.instance.allPuzzlesCompletedEvent -= AllPuzzlesCompleted;
    }
}
