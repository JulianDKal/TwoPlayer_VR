using UnityEngine;
using Unity.Netcode;

public class FireExtinguisherTest : NetworkBehaviour
{
    [Header("Target Objects")]
    public GameObject targetObjectWithParticles; // Parent object with child particle systems
    public Light targetLight; // Light to dim
    public GameObject objectToDisable; // Object to disable at 100% reduction
    public GameObject fakeKey; // Object to disable at 100% reduction
    public GameObject realKey; // Object to disable at 100% reduction

    public GameObject key;
    public float reductionPercentage = 1f; // Reduction per collision
    public NetworkVariable<float> totalReduction = new NetworkVariable<float>(); // Tracks cumulative reduction
    public float reductionFactor = 1;
    public int collisionCount = 0; // Debugging collision counter

    private float initialLightIntensity; // Stores the initial light intensity
    private float[] initialParticleRates;

    private void Start()
    {
        totalReduction.Value = 0f;
        if (targetLight != null)
        {
            initialLightIntensity = targetLight.intensity;
        }
        if (targetObjectWithParticles != null)
        {
            var particleSystems = targetObjectWithParticles.GetComponentsInChildren<ParticleSystem>();
            initialParticleRates = new float[particleSystems.Length];
            for (int i = 0; i < particleSystems.Length; i++)
            {
                initialParticleRates[i] = particleSystems[i].emission.rateOverTime.constant;
            }
        }

        totalReduction.OnValueChanged += (oldValue, newValue) =>
        {
            if (targetObjectWithParticles != null) ReduceEmissionRates();
            if (targetLight != null) AdjustLightIntensity();
            
            if (totalReduction.Value >= 100)
            {
                if(objectToDisable != null) objectToDisable.SetActive(false);
                if(fakeKey != null) fakeKey.SetActive(false);
                if(realKey != null) realKey.SetActive(true);
            }
        };
    }

    private void OnParticleCollision(GameObject other)
    {
        collisionCount++;
        if (IsServer)
        {
            UpdateReduction(reductionPercentage);
        }
        else
        {
            UpdateReductionServerRpc(reductionPercentage);
        }

        // if (totalReduction.Value >= 100 && objectToDisable != null)
        //     objectToDisable.SetActive(false);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void UpdateReductionServerRpc(float reductionAmount)
    {
        UpdateReduction(reductionAmount);
    }
    
    private void UpdateReduction(float reductionAmount)
    {
        totalReduction.Value += reductionAmount;
        reductionFactor = Mathf.Clamp01(1 - totalReduction.Value / 100);

        if (totalReduction.Value >= 100)
        {
            if(objectToDisable != null) objectToDisable.SetActive(false);
            if(fakeKey != null) fakeKey.SetActive(false);
            if(realKey != null) realKey.SetActive(true);
        }
    }

    private void ReduceEmissionRates()
    {
        var particleSystems = targetObjectWithParticles.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particleSystems.Length; i++)
        {
            var ps = particleSystems[i];
            var emission = ps.emission;

            if (emission.enabled)
            {
                // Reduce rate over time (handle each particle system emission once)
                if (emission.rateOverTime.constant > 0)
                {
                    float initialRate = initialParticleRates[i];
                    float newRate = initialRate * reductionFactor;
                    emission.rateOverTime = Mathf.Max(0, newRate);
                }

                // Reduce bursts (apply to each ParticleSystem)
                if (emission.burstCount > 0)
                {
                    var bursts = new ParticleSystem.Burst[emission.burstCount];
                    emission.GetBursts(bursts);

                    for (int j = 0; j < bursts.Length; j++)
                    {
                        bursts[j].count = new ParticleSystem.MinMaxCurve(
                            Mathf.Max(0, bursts[j].count.constant * (1 - reductionPercentage / 100f))
                        );
                    }

                    emission.SetBursts(bursts);
                }
            }
        }
    }

    private void AdjustLightIntensity()
    {
        targetLight.intensity = initialLightIntensity * reductionFactor;
    }
}
