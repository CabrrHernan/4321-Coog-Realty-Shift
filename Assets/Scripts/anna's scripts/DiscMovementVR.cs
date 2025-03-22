using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiscMovementVR : MonoBehaviour
{
    private Vector3 originalPosition;
    private Transform originalParent;

    [SerializeField] private Transform peg3;            // Assign winning peg
    [SerializeField] private int totalDiscs = 3;        // Total number of discs
    [SerializeField] private Transform allDiscsParent;  // Parent holding all discs

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!IsTopDisc())
        {
            Debug.Log("Can't grab this disc. It's not the topmost on its peg.");
            grabInteractable.interactionManager.CancelInteractableSelection(grabInteractable as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable);
            return;
        }

        originalPosition = transform.position;
        originalParent = transform.parent;
        isHeld = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Peg"))
            {
                Transform peg = col.transform;
                if (IsValidMove(peg))
                {
                    transform.SetParent(peg);
                    transform.SetAsLastSibling();
                    transform.position = GetNewPosition(peg);
                    CheckWinCondition();
                    return;
                }
            }
        }

        // Invalid move: reset
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }

    private bool IsTopDisc()
    {
        if (transform.parent == null) return true;

        Transform parentPeg = transform.parent;
        Transform topDisc = null;

        foreach (Transform child in parentPeg)
        {
            if (topDisc == null || child.position.y > topDisc.position.y)
                topDisc = child;
        }

        return topDisc == transform;
    }

    private bool IsValidMove(Transform peg)
    {
        if (peg.childCount == 0) return true;

        Transform topDisc = GetTopDisc(peg);
        if (topDisc != null && transform.localScale.x >= topDisc.localScale.x)
        {
            Debug.Log("Invalid move: can't place larger disc on smaller one.");
            return false;
        }

        return true;
    }

    private Transform GetTopDisc(Transform peg)
    {
        Transform topDisc = null;
        foreach (Transform child in peg)
        {
            if (topDisc == null || child.position.y > topDisc.position.y)
                topDisc = child;
        }
        return topDisc;
    }

    private Vector3 GetNewPosition(Transform peg)
    {
        float discHeight = GetComponent<Renderer>().bounds.size.y;
        float baseY = peg.GetComponent<Collider>().bounds.min.y;

        float newY = baseY + (peg.childCount - 1) * discHeight + (discHeight / 2f);
        return new Vector3(peg.position.x, newY, peg.position.z);
    }

    private void CheckWinCondition()
    {
        if (peg3.childCount == totalDiscs)
        {
            Debug.Log("🎉 You Win!");
            // Trigger win UI if applicable
        }
    }
}
