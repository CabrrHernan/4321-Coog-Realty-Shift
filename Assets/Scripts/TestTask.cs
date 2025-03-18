using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System

public class LightUpTask : MonoBehaviour
{
    public GameObject robot;
    private Light robotLight;

    private void Start()
    {
        if (robot != null)
        {
            robotLight = robot.GetComponentInChildren<Light>();
            if (robotLight != null)
                robotLight.enabled = false;
            else
                Debug.LogWarning("No Light component found on the robot!");
        }
        else
        {
            Debug.LogWarning("Robot reference is missing!");
        }
    }

    public void OnLightUp(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensure the button was pressed
        {
            if (robotLight != null)
            {
                robotLight.enabled = true;
                Debug.Log("Robot has been lit up!");
            }
        }
    }
}
