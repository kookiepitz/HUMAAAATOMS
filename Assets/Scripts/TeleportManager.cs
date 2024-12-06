using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportAnchor; // Assign the teleport location
    public GameObject xrRig;         // Assign the XR Rig (player object)

    public void Teleport()
    {
        if (xrRig != null && teleportAnchor != null)
        {
            // Calculate the offset from the XR Rig's current position to the Camera's position
            Transform xrCamera = xrRig.GetComponentInChildren<Camera>().transform;
            Vector3 offset = xrRig.transform.position - xrCamera.position;

            // Update the XR Rig's position to match the teleport anchor, maintaining the offset
            xrRig.transform.position = teleportAnchor.position + offset;

            // Optionally adjust rotation
            xrRig.transform.rotation = teleportAnchor.rotation;
        }
        else
        {
            Debug.LogError("XR Rig or Teleport Anchor is not assigned!");
        }
    }
}
