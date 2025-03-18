using UnityEngine;

public class RobotController : MonoBehaviour
{
    private Light robotLight;  // Reference to the robot's light component

    private void Start()
    {
        // Get the Light component (make sure it is attached to the robot or its children)
        robotLight = GetComponentInChildren<Light>();

        if (robotLight == null)
        {
            Debug.LogWarning("Robot does not have a Light component!");
        }
        else
        {
            robotLight.enabled = false;  // Initially, the light is off
        }
    }

    private void Update()
    {
        // Detect the left mouse button click (0 = left click, 1 = right click)
        if (Input.GetMouseButtonDown(0))  // 0 for left-click, 1 for right-click
        {
            ToggleRobotLight();  // Turn the light on when the mouse is clicked
        }
    }

    // Method to toggle the robot light
    private void ToggleRobotLight()
    {
        if (robotLight != null)
        {
            robotLight.enabled = !robotLight.enabled;  // Toggle light on/off
            Debug.Log("Robot light toggled! Current state: " + robotLight.enabled);
        }
    }
}
