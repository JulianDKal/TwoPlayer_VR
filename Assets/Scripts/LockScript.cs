using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockScript : MonoBehaviour
{
    public GameObject chain;
    public void KeyInserted(SelectEnterEventArgs args)
    {
        chain.SetActive(false);
        GameManager.instance.puzzlesSolved++;
        if (GameManager.instance.puzzlesSolved >= 3)
        {
            EventManager.instance.allPuzzlesCompleted();
        }
        args.interactableObject.transform.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
