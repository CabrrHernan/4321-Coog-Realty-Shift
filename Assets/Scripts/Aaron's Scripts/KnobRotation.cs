using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class KnobRotation : MonoBehaviour
{
    [Tooltip("Knob level: 0 = Off, 1-6 = Levels")]
    public int currentLevel = 0;

    [Tooltip("Z-axis rotation values for each knob level")]
    public float[] rotationAngles = new float[] { 0f, -72f, -118f, -164f, -209f, -253f, -300f };

    [Tooltip("Rotation animation duration in seconds")]
    public float rotationDuration = 0.3f;

    private Vector3 baseRotation;
    private XRGrabInteractable grabInteractable;
    private Quaternion targetRotation;
    private Quaternion startRotation;
    private float rotationProgress;
    private bool isRotating;

    void Start()
    {
        baseRotation = transform.localEulerAngles;
        targetRotation = transform.localRotation;
        
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnKnobGrabbed);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnKnobGrabbed);
        }
    }

    void Update()
    {
        if (isRotating)
        {
            rotationProgress += Time.deltaTime / rotationDuration;
            
            // Use smooth step for easing
            float t = Mathf.SmoothStep(0f, 1f, rotationProgress);
            
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            
            if (rotationProgress >= 1f)
            {
                isRotating = false;
            }
        }
    }

    private void OnKnobGrabbed(SelectEnterEventArgs args)
    {
        AdvanceKnobLevel();
    }

    private void AdvanceKnobLevel()
    {
        currentLevel++;
        if (currentLevel >= rotationAngles.Length)
            currentLevel = 0;

        float newZ = rotationAngles[currentLevel];
        
        // Set up rotation animation
        startRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(baseRotation.x, baseRotation.y, newZ);
        rotationProgress = 0f;
        isRotating = true;

        Debug.Log($"Knob Level: {currentLevel}");
    }
}