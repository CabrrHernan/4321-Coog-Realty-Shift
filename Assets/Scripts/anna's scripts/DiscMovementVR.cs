using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiscMovementVR : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Tower of Hanoi Settings")]
    [SerializeField] private Transform peg3;            // The goal peg (assign in Inspector)
    [SerializeField] private int totalDiscs = 3;        // Total number of discs in the puzzle

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    private float discHeight;

    protected override void Awake()
    {
        base.Awake();
        discHeight = GetComponent<Renderer>().bounds.size.y;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!IsTopDisc())
        {
            Debug.Log("❌ Cannot grab: Not the top disc.");
            interactionManager.CancelInteractableSelection(this as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable);
            return;
        }

        originalPosition = transform.position;
        originalRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        originalParent = transform.parent;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        Transform nearestPeg = FindNearestPeg();

        if (nearestPeg != null && IsValidMove(nearestPeg))
        {
            // Snap to peg
            transform.SetParent(nearestPeg);
            transform.SetAsLastSibling();
            transform.position = GetNewPosition(nearestPeg);
            transform.rotation = originalRotation;
            CheckWinCondition();
        }
        else
        {
            // Invalid drop → reset
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            transform.SetParent(originalParent);
        }
    }

    public override bool IsSelectableBy(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor)
    {
        return IsTopDisc() && base.IsSelectableBy(interactor);
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
        float baseY = peg.GetComponent<Collider>().bounds.min.y;
        float newY = baseY + (peg.childCount - 1) * discHeight + (discHeight / 2f);
        return new Vector3(peg.position.x, newY, peg.position.z);
    }

    private Transform FindNearestPeg()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.6f); // Radius for peg detection
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Peg"))
            {
                return col.transform;
            }
        }
        return null;
    }

    private void CheckWinCondition()
    {
        if (peg3.childCount == totalDiscs)
        {
            Debug.Log("🎉 You Win!");
            MouseInputUI ui = GameObject.FindObjectOfType<MouseInputUI>();
            if (ui != null)
            {
                ui.ShowWinMessage();
            }
        }
    }
}
