using UnityEngine;

public class HandlePivotController : MonoBehaviour
{
    public Transform pullPole;
    public float maxRotation = 45f;
    public float minY;
    public float maxY;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
        pullPole.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezePositionX | 
            RigidbodyConstraints.FreezePositionZ | 
            RigidbodyConstraints.FreezeRotation;

        if (pullPole != null)
        {
            maxY = pullPole.position.y;
            minY = maxY - 0.2f;  // speed of downward rotation essentially
        }
    }

    void Update()
    {
        if (pullPole == null) return;

        float currentY = Mathf.Clamp(pullPole.position.y, minY, maxY);
        float t = Mathf.InverseLerp(maxY, minY, currentY);

        float angle = Mathf.Lerp(0, maxRotation, t);
        transform.localRotation = initialRotation * Quaternion.Euler(-angle, 0, 0);
    }
}