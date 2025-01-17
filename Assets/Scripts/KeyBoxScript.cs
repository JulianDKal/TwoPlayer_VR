using UnityEngine;

public class KeyBoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.instance.puzzleOneCompletedEvent += DisableMe;
    }

    void DisableMe()
    {
        this.gameObject.SetActive(false);
        EventManager.instance.puzzleOneCompletedEvent -= DisableMe;
    }
}
