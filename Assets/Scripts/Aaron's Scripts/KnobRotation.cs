using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.Events;
using System.Collections;


[RequireComponent(typeof(XRGrabInteractable))]
public class KnobRotation : MonoBehaviour
{
    [Header("Knob Settings")]
    public float[] rotationAngles = new float[] { 0f, -72f, -118f, -164f, -209f, -253f, -300f };
    public float rotationDuration = 0.3f;

    [Header("Events")]
    public UnityEvent<int> onLevelChanged; // Notify other components

    private int _currentLevel = 0;
    private Vector3 _baseRotation;
    private XRGrabInteractable _grabInteractable;
    private Coroutine _rotationCoroutine;

    public string CurrentLevelName => _currentLevel == 0 ? "Off" : $"Level {_currentLevel-1}";
    public int CurrentLevel => _currentLevel;

    void Start()
    {
        _baseRotation = transform.localEulerAngles;
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnKnobGrabbed);
    }

    private void OnKnobGrabbed(SelectEnterEventArgs args)
    {
        AdvanceKnobLevel();
    }

    private void AdvanceKnobLevel()
    {
        _currentLevel = (_currentLevel + 1) % rotationAngles.Length;
        
        if (_rotationCoroutine != null) 
            StopCoroutine(_rotationCoroutine);
        
        _rotationCoroutine = StartCoroutine(RotateKnobSmoothly());
        onLevelChanged.Invoke(_currentLevel);
    }

    private IEnumerator RotateKnobSmoothly()
    {
        float elapsed = 0f;
        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(_baseRotation.x, _baseRotation.y, rotationAngles[_currentLevel]);

        while (elapsed < rotationDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRot, targetRot, 
                Mathf.SmoothStep(0f, 1f, elapsed/rotationDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRot;
    }
}