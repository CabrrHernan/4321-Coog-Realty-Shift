using UnityEngine;

public class DiscMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private Vector3 offset;
    private bool isDragging = false;
    // private Camera cam;

    [SerializeField] private Transform peg3; // Assign in Inspector
    [SerializeField] private int totalDiscs; // Set total number of discs
    [SerializeField] private Transform allDiscsParent; // Assign the parent holding all discs
    [SerializeField] private Camera cam; // Drag the camera in Inspector

    private float discHeight = 0.5f; // Will be dynamically assigned
    private float stackSpacing = 0.1f; // Will be dynamically assigned

    void Start()
    {
        if (cam == null) // Checks if it's assigned in Inspector
        {
            cam = Camera.main; // Try assigning automatically
        }

        // Get the disc height
        Renderer discRenderer = GetComponent<Renderer>();
        if (discRenderer != null)
        {
            discHeight = discRenderer.bounds.size.y;
        }

    }

    void AssignDynamicStackSpacing()
    {
        if (allDiscsParent == null)
        {
            Debug.LogWarning("allDiscsParent is not assigned. Stack spacing may not be optimized.");
            return;
        }

        float minDiscHeight = float.MaxValue;
        foreach (Transform disc in allDiscsParent)
        {
            Renderer renderer = disc.GetComponent<Renderer>();
            if (renderer != null)
            {
                float height = renderer.bounds.size.y;
                if (height < minDiscHeight)
                {
                    minDiscHeight = height;
                }
            }
        }

    }

    void OnMouseDown()
    {

        Debug.Log($"Clicked on {name}");
        if (!IsTopDisc())
        {
            Debug.Log("Invalid move: Cannot move this disc, it is not the topmost disc!");
            return;
        }

        isDragging = true;
        originalPosition = transform.position;
        originalParent = transform.parent;
        offset = transform.position - GetMouseWorldPosition();
    }


    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Peg"))
            {
                Transform peg = col.transform;
                if (IsValidMove(peg))
                {
                    // Move the disc and parent it properly
                    transform.SetParent(peg);
                    transform.SetAsLastSibling(); // Ensures proper hierarchy order
                    transform.position = GetNewPosition(peg);
                    CheckWinCondition();
                    return;
                }
            }
        }

        // Reset if move is invalid
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }

    bool IsTopDisc()
    {
        if (transform.parent == null) return true; // A disc with no parent is by definition the topmost.

        Transform parentPeg = transform.parent;
        Transform topDisc = null;

        // Iterate through all children and find the highest disc (assuming vertical stacking)
        foreach (Transform child in parentPeg)
        {
            if (topDisc == null || child.position.y > topDisc.position.y)
            {
                topDisc = child;
            }
        }

        Debug.Log($"Checking if {name} is the top disc. Parent Peg: {parentPeg.name}, Top Disc Found: {topDisc?.name}");

        return topDisc == transform;
    }



    bool IsValidMove(Transform peg)
    {
        if (peg.childCount == 0) return true;

        Transform topDisc = GetTopDisc(peg);
        if (topDisc != null && transform.localScale.x >= topDisc.localScale.x)
        {
            Debug.LogWarning("Invalid move: Cannot place a larger disc on top of a smaller disc!");
            return false;
        }

        return true;
    }

    Vector3 GetNewPosition(Transform peg)
    {
        float newY;

        if (peg.childCount == 1)
        {
            // If this is the first disc on the peg, place it at the bottom
            newY = peg.GetComponent<Collider>().bounds.min.y + (discHeight / 2);
        }
        else
        {
            newY = peg.GetComponent<Collider>().bounds.min.y + (peg.childCount - 1) * discHeight + (discHeight / 2);
        }

        return new Vector3(peg.position.x, newY, peg.position.z);
    }

    Transform GetTopDisc(Transform peg)
    {
        Transform topDisc = null;

        foreach (Transform child in peg)
        {
            if (topDisc == null || child.position.y > topDisc.position.y)
            {
                topDisc = child;
                Debug.Log($"Top disc found: {topDisc?.name}");
            }
        }
        return topDisc;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    void CheckWinCondition()
    {
        if (peg3.childCount == totalDiscs)
        {
            Debug.Log("You Win!");
            // Show "You Win" UI
            GameObject.Find("Canvas").GetComponent<MouseInputUI>().ShowWinMessage();
        }
    }
}
