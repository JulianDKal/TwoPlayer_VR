using System.Runtime.CompilerServices;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 maxOffset; // Erlaubt unterschiedliche Offsets für x, y und z.

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Begrenze die Position auf der x-Achse
        float clampedX = Mathf.Clamp(transform.position.x, startPos.x + Mathf.Min(0, maxOffset.x), startPos.x + Mathf.Max(0, maxOffset.x));

        // Begrenze die Position auf der y-Achse
        float clampedY = Mathf.Clamp(transform.position.y, startPos.y + Mathf.Min(0, maxOffset.y), startPos.y + Mathf.Max(0, maxOffset.y));

        // Begrenze die Position auf der z-Achse
        float clampedZ = Mathf.Clamp(transform.position.z, startPos.z + Mathf.Min(0, maxOffset.z), startPos.z + Mathf.Max(0, maxOffset.z));

        // Setze die begrenzte Position zurück
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}
