using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

public class PeriodicTableToggle : MonoBehaviour
{
    public GameObject periodicTablePanel; 
    private bool isVisible = true;

    public InputActionReference toggleAction; //

    void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.Enable();
            toggleAction.action.performed += OnTogglePressed;
        }
    }

    void OnDisable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.performed -= OnTogglePressed;
        }
    }

    private void OnTogglePressed(InputAction.CallbackContext context)
    {
        isVisible = !isVisible;
        periodicTablePanel.SetActive(isVisible);
        Debug.Log($"[VR] Toggled Periodic Table: {(isVisible ? "ON" : "OFF")}");
    }
}
