using System;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private SphereCollider handleCollider;
    private void Start()
    {
        EventManager.instance.allPuzzlesCompletedEvent += AllPuzzlesCompleted;
        
        rigidbody = GetComponent<Rigidbody>();
        handleCollider = GetComponentInChildren<SphereCollider>();
        
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        handleCollider.enabled = false;
    }

    private void AllPuzzlesCompleted()
    {
        Debug.Log("All puzzles completed, final door is open!!");
        handleCollider.enabled = true;
        rigidbody.constraints = RigidbodyConstraints.None;
        EventManager.instance.allPuzzlesCompletedEvent -= AllPuzzlesCompleted;
    }
}
