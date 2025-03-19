using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BeakerPourClick : MonoBehaviour
{
    public Transform targetBeaker; // Assign larger beaker in Inspector
    public float pourDuration = 1.5f;
    private bool isPouring = false;
    public InputActionReference pourAction; // Assign this in the Inspector
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }

        if (pourAction != null)
        {
            pourAction.action.performed += _ => TryPouring();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Beaker Picked Up: " + gameObject.name);
        HoverTextManager.instance.ShowText("Holding: " + gameObject.name);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Beaker Released: " + gameObject.name);
        HoverTextManager.instance.ShowText("No Object");
    }

    private void TryPouring()
    {
        if (isPouring) return;

        Debug.Log("Attempting to Pour...");

        if (IsHoveringOverTargetBeaker())
        {
            Debug.Log("Correct Pour Target Detected!");
            StartCoroutine(AnimatePour());
        }
        else
        {
            Debug.Log("No valid target detected for pouring.");
        }
    }

    bool IsHoveringOverTargetBeaker()
    {
        // Cast a ray from the controller to see if it's pointing at the large beaker
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);
            return hit.collider.gameObject == targetBeaker.gameObject;
        }

        return false;
    }

    IEnumerator AnimatePour()
    {
        isPouring = true;
        Debug.Log("Pour Animation Started...");

        Quaternion startRotation = transform.rotation;
        Quaternion pourRotation = Quaternion.Euler(transform.eulerAngles.x - 60, transform.eulerAngles.y, transform.eulerAngles.z);

        float elapsed = 0f;
        while (elapsed < pourDuration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, pourRotation, elapsed / pourDuration);
            yield return null;
        }

        Debug.Log("Liquid Poured into: " + targetBeaker.gameObject.name);

        // Notify larger beaker that liquid is added
        targetBeaker.GetComponent<ChemicalMixer>().AddChemical(GetComponent<BeakerHover>().chemicalName);

        // Reset Beaker Position
        yield return new WaitForSeconds(1);
        transform.rotation = startRotation;
        isPouring = false;
    }
}
