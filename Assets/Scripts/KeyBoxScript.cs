using UnityEngine;
public class KeyBoxScript : MonoBehaviour
{
    public GameObject key;
    public Transform lidTransform;
    public Collider lidCollider;
    public float openSpeed = 50f;
    public float openAngle = -95f;
    public bool isOpening = false;
    private float currentAngle = 0f;

    void Start()
    {
        EventManager.instance.puzzleOneCompletedEvent += DisableMe;
        BoxCollider myCollider = gameObject.GetComponent<BoxCollider>();
        BoxCollider[] colliders = key.GetComponents<BoxCollider>();
        foreach (BoxCollider keyCollider in colliders)
        {
            Physics.IgnoreCollision(myCollider, keyCollider);
        }
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
            lidTransform.Rotate(0f, 0f, -rotationStep);
            currentAngle += rotationStep;
        }
    }

    public void DisableMe()
    {
        if (lidCollider != null)
        {
            lidCollider.enabled = false;
        }
        isOpening = true;
        EventManager.instance.puzzleOneCompletedEvent -= DisableMe;
    }
}