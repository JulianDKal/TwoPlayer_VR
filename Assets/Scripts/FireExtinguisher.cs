using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem foam;
    
    void Start()
    {
        foam.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableParticles(ActivateEventArgs arg)
    {
        foam.Play();
    }

    public void DisableParticles(DeactivateEventArgs arg)
    {
        foam.Stop();
    }
}
