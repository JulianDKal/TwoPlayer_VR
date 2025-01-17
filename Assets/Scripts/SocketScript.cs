using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketScript : MonoBehaviour
{
    public string objectName;
    private bool correctObjectSet = false;

    public void CorrectObjectAttached(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.gameObject.name == objectName)
        {
            Debug.Log("Correct Object attached!" + args.interactableObject.transform.gameObject.name);
            correctObjectSet = true;
            GameManager.instance.puzzleOneScore++;
            if (GameManager.instance.puzzleOneScore == 3)
            {
                EventManager.instance.puzzleOneCompleted();
            }
        }
    }

    public void ObjectDetached(SelectExitEventArgs args)
    {
        if (correctObjectSet == true)
        {
            GameManager.instance.puzzleOneScore--;
            correctObjectSet = false;
        }
    }
}
