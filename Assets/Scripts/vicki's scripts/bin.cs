using UnityEngine;

public class Container : MonoBehaviour
{
    public string expectedRockType; // The type of rock this container expects
    public string expectedBin;     // The expected bin ("Small" or "Big")

    private Rock placedRock; // Reference to the rock currently placed in the container
    private sortingTaskManager taskManager; // Reference to the task manager

    private void Start()
    {
        // Find the sortingTaskManager in the scene
        taskManager = FindFirstObjectByType<sortingTaskManager>();
    }

    // Detect when a rock enters the container
    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null)
        {
            placedRock = rock; // Store the reference to the placed rock
            // Debug.Log("Rock placed in container: " + rock.rockType);

            // Increase the number of rocks in bins
            if (taskManager != null)
            {
                taskManager.IncreaseRockInBin();
                taskManager.CheckCompletion(); // Check if the task is complete
            }
        }
    }

    // Detect when a rock exits the container
    private void OnTriggerExit(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null && rock == placedRock)
        {
            placedRock = null; // Clear the reference when the rock is removed
            // Debug.Log("Rock removed from container: " + rock.rockType);

            // Decrease the number of rocks in bins
            if (taskManager != null)
            {
                taskManager.DecreaseRockInBin();
                taskManager.CheckCompletion(); // Check if the task is complete
            }
        }
    }

    // Check if the placed rock matches the expected type and bin
    public bool CheckRock()
    {
        if (placedRock == null)
        {
            return false; // No rock placed
        }

        return placedRock.rockType == expectedRockType && placedRock.GetCorrectBin() == expectedBin;
    }
}