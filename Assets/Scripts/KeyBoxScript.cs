using UnityEngine;

public class KeyBoxScript : MonoBehaviour
{
    public Transform coverTransform;
    public Collider coverCollider;
    public Collider baseCollider;
    public float openSpeed = 50f;
    public float openAngle = -95f;
    public bool isOpening = false;
    private float currentAngle = 0f;

    private void Start()
    {
        EventManager.instance.puzzleOneCompletedEvent += DisableMe;
    }
    
    void Update()
    {
        if (isOpening)
        {
            // Berechnet die zu rotierende Menge für diesen Frame
            float rotationStep = openSpeed * Time.deltaTime;
            
            // Stoppt bei der maximalen Öffnung
            if (currentAngle + rotationStep > Mathf.Abs(openAngle))
            {
                rotationStep = Mathf.Abs(openAngle) - currentAngle;
                isOpening = false;
            }
            
            // Rotiert den Deckel um die Z-Achse
            coverTransform.Rotate(0f, 0f, -rotationStep);
            currentAngle += rotationStep;
        }
    }

    public void DisableMe()
    {
        if (coverCollider != null)
        {
            coverCollider.enabled = false;
            baseCollider.enabled = false;
        }
        isOpening = true;
        EventManager.instance.puzzleOneCompletedEvent -= DisableMe;
    }
}