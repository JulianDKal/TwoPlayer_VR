using UnityEngine;

public class PlanePositioner : MonoBehaviour
{
    public float distanceFromGround = 2.0f; // Abstand vom Boden
    public string ignoredLayer = "Player";  // Name des Layers, der ignoriert werden soll

    private void Update()
    {
        AdjustPlanePosition();
    }

    private void AdjustPlanePosition()
    {
        // Ray von oberhalb der aktuellen Position des Objekts nach unten
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
        RaycastHit hit;

        // Bitmaske für den ignorierten Layer erstellen
        int layerToIgnore = LayerMask.NameToLayer(ignoredLayer);
        int layerMask = ~(1 << layerToIgnore); // Alle Layer außer dem ignorierten Layer

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Positioniere das Objekt in der gewünschten Höhe über dem Boden
            Vector3 newPosition = hit.point + Vector3.up * distanceFromGround;
            transform.position = newPosition;
        }
    }
}
