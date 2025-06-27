using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    void Update()
    {
        PortalCameraController();
    }

    void PortalCameraController()
    {
        Vector3 playerOffset = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffset;

        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCameraDirection = portalRotationDiff * playerCamera.forward;

        newCameraDirection = new Vector3(
            newCameraDirection.x *-1,
            newCameraDirection.y,
            newCameraDirection.z * -1
            );

        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
