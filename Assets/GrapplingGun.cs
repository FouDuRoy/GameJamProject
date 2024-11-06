using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    private PlayerInputHandler inputHandler;
    public Transform guntip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool isGrappling = false;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        inputHandler = PlayerInputHandler.Instance;
    }

    private void FixedUpdate()
    {
        DrawRope();
        if (inputHandler.GrabTriggered && !isGrappling)
            StartGrapple();
        else if (!inputHandler.GrabTriggered && isGrappling)
            StopGrapple();
    }
    /// <summary>
    /// Start the grappling process
    /// </summary>
    void StartGrapple()
    {
        Debug.Log("Hit");
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = camera.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            isGrappling = true; // Set isGrappling to true to prevent re-triggering
        }
    }

    void DrawRope()
    {
        lr.SetPosition(0, guntip.position);
        lr.SetPosition(1, grapplePoint);
    }

    /// <summary>
    /// Stop the grappling process
    /// </summary>
    void StopGrapple()
    {
        Destroy(joint); // Destroy the spring joint
        isGrappling = false; // Reset isGrappling to allow grappling to start again
    }
}
