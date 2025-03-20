using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FaucetController : MonoBehaviour
{
    public ParticleSystem waterParticleSystem; // Reference to the particle system
    public Transform faucetHandleTransform;    // Reference to the handle transform
    public float handleRotationAngle = 50f;    // How far the handle rotates
    public float rotationSpeed = 5f;           // Speed of rotation

    private XRGrabInteractable grabInteractable;
    private bool isFaucetOn = false;
    private Quaternion handleRotationOff;
    private Quaternion handleRotationOn;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        // Store initial rotations
        handleRotationOff = faucetHandleTransform.localRotation;
        handleRotationOn = handleRotationOff * Quaternion.Euler(handleRotationAngle, 0, 0); // Change axis if needed
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        ToggleFaucet();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Optional: logic for handle release
    }

    private void ToggleFaucet()
    {
        isFaucetOn = !isFaucetOn;

        if (isFaucetOn)
            waterParticleSystem.Play();
        else
            waterParticleSystem.Stop();
    }

    private void Update()
    {
        // Smoothly rotate the handle depending on state
        Quaternion targetRotation = isFaucetOn ? handleRotationOn : handleRotationOff;
        faucetHandleTransform.localRotation = Quaternion.Lerp(faucetHandleTransform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }
}
