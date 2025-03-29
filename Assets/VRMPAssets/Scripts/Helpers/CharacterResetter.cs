using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace XRMultiplayer
{
    public class CharacterResetter : MonoBehaviour
    {
        public Vector2 minMaxHeight = new Vector2(-2.5f, 25.0f);
        public float resetDistance = 75.0f;
        public Transform resetPos;
        public Transform newResetPos;

        TeleportationProvider teleportationProvider;
        Vector3 resetPosition;

        private void Start()
        {
            teleportationProvider = GetComponentInChildren<TeleportationProvider>();
            resetPosition = resetPos.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < minMaxHeight.x)
            {
                ResetPlayer();
            }
            else if (transform.position.y > minMaxHeight.y)
            {
                ResetPlayer();
            }
            if (Mathf.Abs(transform.position.x) > resetDistance || Mathf.Abs(transform.position.z) > resetDistance)
            {
                ResetPlayer();
            }
        }

        public void ResetPlayer()
        {
            ResetPlayer(resetPosition);
        }

        void ResetPlayer(Vector3 destination)
        {
            TeleportRequest teleportRequest = new()
            {
                destinationPosition = destination,
                destinationRotation = Quaternion.identity
            };

            if (!teleportationProvider.QueueTeleportRequest(teleportRequest))
            {
                Utils.LogWarning("Failed to queue teleport request");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SpawnPointChanger"))
            {
                resetPosition = newResetPos.position;
            }
        }
    }
}
