using System.Runtime.CompilerServices;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    private Vector3 startPos;
    public float maxOffset;

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if (transform.position.x > (startPos.x + maxOffset))
        {
            transform.position = new Vector3(startPos.x + maxOffset, startPos.y, startPos.z);
        }
        else if (transform.position.x < startPos.x)
        {
            transform.position = startPos;
        }
    }
}
