using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    


    public float moveSpeed = 5f;
    public float groundOffset = 0.1f;
    public float groundCheckDistance = 5f;
    public LayerMask groundLayer;

    private Vector3 currentForward;

    void Start()
    {
        currentForward = transform.forward;
    }

    void Update()
    {
        // Step 1: Check ground directly under tornado
        if (Physics.Raycast(transform.position + Vector3.up * groundCheckDistance, Vector3.down, out RaycastHit groundHit, groundCheckDistance * 2f, groundLayer))
        {
            // Step 2: Move along the slope direction
            // Use the ground normal to project forward movement onto the slope surface
            Vector3 slopeDirection = Vector3.ProjectOnPlane(currentForward, groundHit.normal).normalized;

            // Move along the slope
            Vector3 nextPos = transform.position + slopeDirection * moveSpeed * Time.deltaTime;

            // Step 3: Stick to ground height
            if (Physics.Raycast(nextPos + Vector3.up * groundCheckDistance, Vector3.down, out RaycastHit nextGroundHit, groundCheckDistance * 2f, groundLayer))
            {
                nextPos.y = nextGroundHit.point.y + groundOffset;

                // Step 4: Adjust rotation to match terrain slope
                transform.rotation = Quaternion.LookRotation(slopeDirection, nextGroundHit.normal);
            }

            transform.position = nextPos;
            currentForward = slopeDirection; // store new forward for smoother turning
        }
    }


}
