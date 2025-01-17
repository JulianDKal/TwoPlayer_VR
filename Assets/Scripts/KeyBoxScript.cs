using UnityEngine;

public class KeyBoxScript : MonoBehaviour
{
    public GameObject key;
    void Start()
    {
        EventManager.instance.puzzleOneCompletedEvent += DisableMe;
        BoxCollider keyCollider = key.GetComponent<BoxCollider>();
        BoxCollider myCollider = gameObject.GetComponent<BoxCollider>();
        
        Physics.IgnoreCollision(myCollider, keyCollider);
    }

    void DisableMe()
    {
        this.gameObject.SetActive(false);
        EventManager.instance.puzzleOneCompletedEvent -= DisableMe;
    }
}
