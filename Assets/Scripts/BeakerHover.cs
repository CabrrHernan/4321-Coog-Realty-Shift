using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BeakerHover : MonoBehaviour
{
    public string chemicalName; // Set this in Inspector (e.g., "Red Chemical")

    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(); // Auto-add if missing
        }

        // Subscribe to XR interaction events
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        HoverTextManager.instance.ShowText(chemicalName);
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        HoverTextManager.instance.HideText();
    }
}
