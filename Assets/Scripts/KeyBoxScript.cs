using UnityEngine;

public class KeyBoxScript : MonoBehaviour
{
    public GameObject key;
    void Start()
    {
        EventManager.instance.puzzleOneCompletedEvent += DisableMe;
        BoxCollider myCollider = gameObject.GetComponent<BoxCollider>();
        BoxCollider[] colliders = key.GetComponents<BoxCollider>();
        foreach (BoxCollider keyCollider in colliders)
        {
            Physics.IgnoreCollision(myCollider, keyCollider);
            //Debug.Log($"Found BoxCollider: {collider}");
        }
        
    }

    void DisableMe()
    {
        this.gameObject.SetActive(false);
        EventManager.instance.puzzleOneCompletedEvent -= DisableMe;
    }
}
