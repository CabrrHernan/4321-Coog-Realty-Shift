using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic; // Add this namespace for List

public class ShootLaser : MonoBehaviour
{
    public Material material;

    private bool isLaserActive = false; // Toggle state for the laser

    // Update is called once per frame
    void Update()
    {
        // Check if the A button on the right-hand controller is pressed
        if (IsAButtonPressed())
        {
            isLaserActive = !isLaserActive; // Toggle the laser state
        }

        // If the laser is active, create and manage the laser
        if (isLaserActive)
        {
            GameObject existingLaser = GameObject.Find("Laser Beam");
            if (existingLaser != null)
            {
                Destroy(existingLaser);
            }

            GameObject laserObj = new GameObject("Laser Beam");
            LaserBeam beam = laserObj.AddComponent<LaserBeam>();
            beam.Initialize(gameObject.transform.position, gameObject.transform.forward, material);
        }
        else
        {
            // If the laser is inactive, destroy any existing laser
            GameObject existingLaser = GameObject.Find("Laser Beam");
            if (existingLaser != null)
            {
                Destroy(existingLaser);
            }
        }
    }

    // Helper method to check if the A button is pressed
    private bool IsAButtonPressed()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, inputDevices);

        foreach (var device in inputDevices)
        {
            bool isPressed;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed) && isPressed)
            {
                return true;
            }
        }
        return false;
    }
}
