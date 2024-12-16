using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyScript : MonoBehaviour
{
    private XRBaseInteractable interactable;
    Animator animator;
    private Renderer objectRenderer;
    private Color originalColor;
    
    private Color hoverColor = Color.yellow;
    private Color selectColor = Color.green;
    private Color activateColor = Color.red;
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        animator = GetComponent<Animator>();
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
        
        interactable.activated.AddListener(OnActivate);
        interactable.deactivated.AddListener(OnDeactivate);
    }

    private void OnHoverEnter(HoverEnterEventArgs hoverEnterEventArgs)
    {
        objectRenderer.material.color = hoverColor;
    }

    private void OnHoverExit(HoverExitEventArgs hoverExitEventArgs)
    {
        objectRenderer.material.color = originalColor;
    }

    private void OnSelectEnter(SelectEnterEventArgs selectEnterEventArgs)
    {
        Debug.Log("Object grabbed");
        animator.SetBool("grabbed", true);
        objectRenderer.material.color = selectColor;
    }

    private void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
    {
        Debug.Log("Object was let go");
        animator.SetBool("grabbed", false);
        objectRenderer.material.color = originalColor;
    }

    private void OnActivate(ActivateEventArgs activateEventArgs)
    {
        Debug.Log("Activate action performed!");
        objectRenderer.material.color = activateColor;
    }

    private void OnDeactivate(DeactivateEventArgs deactivateEventArgs)
    {
        Debug.Log("Deactivate action performed!");
        objectRenderer.material.color = originalColor;
    }
}
