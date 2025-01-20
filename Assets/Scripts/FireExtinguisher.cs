using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem particleSystem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        // grabbable.activated.AddListener(EnableParticles);
        // grabbable.deactivated.AddListener(DisableParticles);
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableParticles(ActivateEventArgs arg)
    {
        // if (!particleSystem.isPlaying)
        // {
            particleSystem.Play();
        // }
    }

    public void DisableParticles(DeactivateEventArgs arg)
    {
        // if (particleSystem.isPlaying)
        // {
            particleSystem.Stop();
        // }
    }
}
