using UnityEngine;

public class Drawer : MonoBehaviour
{
    private Vector3 startLocalPos;
    private Transform originalParent;
    public Vector3 maxLocalOffset;
   
    void Start()
    {
        startLocalPos = transform.localPosition;
        originalParent = transform.parent;
    }

    void Update()
    {
        // Restore original parent if changed
        if (transform.parent != originalParent)
        {
            transform.SetParent(originalParent, true);
        }

        // Clamp local position
        Vector3 clampedLocalPos = new Vector3(
            Mathf.Clamp(transform.localPosition.x,
                startLocalPos.x + Mathf.Min(0, maxLocalOffset.x),
                startLocalPos.x + Mathf.Max(0, maxLocalOffset.x)),
            Mathf.Clamp(transform.localPosition.y,
                startLocalPos.y + Mathf.Min(0, maxLocalOffset.y),
                startLocalPos.y + Mathf.Max(0, maxLocalOffset.y)),
            Mathf.Clamp(transform.localPosition.z,
                startLocalPos.z + Mathf.Min(0, maxLocalOffset.z),
                startLocalPos.z + Mathf.Max(0, maxLocalOffset.z))
        );

        // Set local position
        transform.localPosition = clampedLocalPos;
    }
}