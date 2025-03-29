using UnityEngine;

public class CorrectDrawerLimit : MonoBehaviour
{
    [Tooltip("Maximale Verschiebungsgrenze des Objekts")]
    public Vector3 maxOffset = Vector3.zero;

    [Tooltip("Minimale Verschiebungsgrenze des Objekts")]
    public Vector3 minOffset = Vector3.zero;

    public bool setIsKinematic;

    private ConfigurableJoint jointx;
    private ConfigurableJoint jointy;
    private ConfigurableJoint jointz;

    void Awake()
    {
        if (maxOffset.x - minOffset.x != 0)
        {
            jointx = gameObject.AddComponent<ConfigurableJoint>();

            SoftJointLimit xLimit = new SoftJointLimit
            {
                limit = (maxOffset.x - minOffset.x)/2,
                contactDistance = 0.1f
            };

            jointx.xMotion = ConfigurableJointMotion.Limited;
            jointx.yMotion = ConfigurableJointMotion.Locked;
            jointx.zMotion = ConfigurableJointMotion.Locked;
            jointx.angularXMotion = ConfigurableJointMotion.Locked;
            jointx.angularYMotion = ConfigurableJointMotion.Locked;
            jointx.angularZMotion = ConfigurableJointMotion.Locked;

            jointx.autoConfigureConnectedAnchor = false;
            jointx.anchor = Vector3.zero;
            jointx.axis = Vector3.zero;
            jointx.secondaryAxis = Vector3.zero;

            jointx.linearLimit = xLimit;

            Vector3 localLimit = new Vector3((minOffset.x + maxOffset.x) / 2, 0, 0);
            Vector3 worldLimit = transform.TransformPoint(localLimit);
            jointx.connectedAnchor = worldLimit;
        }
        if (maxOffset.y - minOffset.y != 0)
        {
            jointy = gameObject.AddComponent<ConfigurableJoint>();

            SoftJointLimit yLimit = new SoftJointLimit
            {
                limit = (maxOffset.y - minOffset.y)/2,
                contactDistance = 0.1f
            };

            jointy.xMotion = ConfigurableJointMotion.Locked;
            jointy.yMotion = ConfigurableJointMotion.Limited;
            jointy.zMotion = ConfigurableJointMotion.Locked;
            jointy.angularXMotion = ConfigurableJointMotion.Locked;
            jointy.angularYMotion = ConfigurableJointMotion.Locked;
            jointy.angularZMotion = ConfigurableJointMotion.Locked;

            jointy.autoConfigureConnectedAnchor = false;
            jointy.anchor = Vector3.zero;
            jointy.axis = Vector3.zero;
            jointy.secondaryAxis = Vector3.zero;

            jointy.linearLimit = yLimit;

            Vector3 localLimit = new Vector3(0, (minOffset.y + maxOffset.y) / 2, 0);
            Vector3 worldLimit = transform.TransformPoint(localLimit);
            jointy.connectedAnchor = worldLimit;
        }
        if (maxOffset.z - minOffset.z != 0)
        {
            jointz = gameObject.AddComponent<ConfigurableJoint>();

            SoftJointLimit zLimit = new SoftJointLimit
            {
                limit = (maxOffset.z - minOffset.z)/2,
                contactDistance = 0.1f
            };

            jointz.xMotion = ConfigurableJointMotion.Locked;
            jointz.yMotion = ConfigurableJointMotion.Locked;
            jointz.zMotion = ConfigurableJointMotion.Limited;
            jointz.angularXMotion = ConfigurableJointMotion.Locked;
            jointz.angularYMotion = ConfigurableJointMotion.Locked;
            jointz.angularZMotion = ConfigurableJointMotion.Locked;

            jointz.autoConfigureConnectedAnchor = false;
            jointz.anchor = Vector3.zero;
            jointz.axis = Vector3.zero;
            jointz.secondaryAxis = Vector3.zero;

            jointz.linearLimit = zLimit;

            Vector3 localLimit = new Vector3(0, 0, (minOffset.z + maxOffset.z) / 2);
            Vector3 worldLimit = transform.TransformPoint(localLimit);
            jointz.connectedAnchor = worldLimit;
        }
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = setIsKinematic;
        }
    }
}