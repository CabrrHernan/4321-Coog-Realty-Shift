using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody), typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class SpringBackPosition : MonoBehaviour
{
    private Vector3 startingPosition;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isGrabbed = false;
    private float originalDrag;
    private SpringJoint springJoint;
    public float springStrength = 100f;
    public float springDamper = 25f;

    public float maxHeightOffset = 0.05f;
    public float positionConstraintStrength = 50f;

    public float grabbedDrag = 1f;
    public float throwVelocityScale = 1.5f;

    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        originalDrag = rb.linearDamping;
        
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
        grabInteractable.trackPosition = true;
        grabInteractable.trackRotation = true;
        grabInteractable.throwOnDetach = true;
        grabInteractable.throwVelocityScale = throwVelocityScale;

        // Configure collider for better interaction
        if (TryGetComponent<Collider>(out var collider))
        {
            collider.material = new PhysicsMaterial
            {
                dynamicFriction = 0.4f,
                staticFriction = 0.4f,
                bounciness = 0.1f,
                frictionCombine = PhysicsMaterialCombine.Average,
                bounceCombine = PhysicsMaterialCombine.Average
            };
        }

        // Configure spring joint
        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = startingPosition;
        springJoint.spring = springStrength;
        springJoint.damper = springDamper;
        springJoint.maxDistance = 0.1f;
        springJoint.enableCollision = true;

        // Optimize rigidbody for interaction
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnGrabStarted(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        rb.linearDamping = grabbedDrag;
        rb.isKinematic = false; // Ensure kinematic is off when grabbed
    }

    void OnGrabEnded(SelectExitEventArgs args)
    {
        isGrabbed = false;
        rb.linearDamping = originalDrag;
        springJoint.connectedAnchor = startingPosition;
    }

    void FixedUpdate()
    {
        if (!isGrabbed) return;

        // Height constraint
        float heightAbove = transform.position.y - (startingPosition.y + maxHeightOffset);
        if (heightAbove > 0)
        {
            rb.AddForce(Vector3.down * (heightAbove * positionConstraintStrength), 
                      ForceMode.Acceleration);
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabStarted);
            grabInteractable.selectExited.RemoveListener(OnGrabEnded);
        }
    }
}