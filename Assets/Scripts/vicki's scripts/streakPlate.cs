using UnityEngine;

public class StreakPlate : MonoBehaviour
{
    private Renderer plateRenderer; // Renderer for the streak plate
    private Color originalColor;    // Original color of the streak plate
    private bool isMarkActive = false; // Track if a mark is currently active

    void Start()
    {
        // Get the Renderer component and store the original color
        plateRenderer = GetComponent<Renderer>();
        if (plateRenderer != null)
        {
            originalColor = plateRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a rock
        Rock rock = other.GetComponent<Rock>();
        if (rock != null && !isMarkActive)
        {
            // Change the streak plate's color to the rock's streak color
            plateRenderer.material.color = rock.GetStreakColor();

            // Start a coroutine to reset the color after 3 seconds
            StartCoroutine(ResetStreakColorAfterDelay(3f));
            isMarkActive = true; // Mark is now active
        }
    }

    private System.Collections.IEnumerator ResetStreakColorAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Reset the streak plate's color to the original color
        plateRenderer.material.color = originalColor;
        isMarkActive = false; // Mark is no longer active
    }
}