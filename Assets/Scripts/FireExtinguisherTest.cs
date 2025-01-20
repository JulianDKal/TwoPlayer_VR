using UnityEngine;

public class FireExtinguisherTest : MonoBehaviour
{
    [Header("Target Objects")]
    public GameObject targetObjectWithParticles; // Parent object with child particle systems
    public Light targetLight; // Light to dim
    public GameObject objectToDisable; // Object to disable at 100% reduction

    public float reductionPercentage = 1f; // Reduction per collision
    public float totalReduction = 0f; // Tracks cumulative reduction
    public float reductionFactor = 1;
    public int collisionCount = 0; // Debugging collision counter

    private float initialLightIntensity; // Stores the initial light intensity
    private float[] initialParticleRates;

    private void Start()
    {
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
    }

    private void OnParticleCollision(GameObject other)
    {
        collisionCount++;
        totalReduction += reductionPercentage;
        reductionFactor = Mathf.Clamp01(1 - totalReduction / 100);

        if (targetObjectWithParticles != null)
            ReduceEmissionRates();

        if (targetLight != null)
            AdjustLightIntensity();

        if (totalReduction >= 100 && objectToDisable != null)
            objectToDisable.SetActive(false);
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
