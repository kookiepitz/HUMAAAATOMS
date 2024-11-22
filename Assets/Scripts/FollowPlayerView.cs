using UnityEngine;

public class FixedPositionFollow : MonoBehaviour
{
    public Transform playerCamera; // Assign the VR headset's camera (e.g., XR Rig's Camera)
    public Vector3 offset;         // Offset position relative to the player's POV

    void LateUpdate()
    {
        if (playerCamera != null)
        {
            // Fix the canvas position relative to the player's camera
            transform.position = playerCamera.position + playerCamera.forward * offset.z +
                                 playerCamera.right * offset.x +
                                 playerCamera.up * offset.y;

            // Make the canvas face the player's camera
            transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
        }
    }
}
