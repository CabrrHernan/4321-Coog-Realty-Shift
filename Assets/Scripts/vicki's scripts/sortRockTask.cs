using UnityEngine;
using UnityEngine.UI;

public class sortingTaskManager : MonoBehaviour
{
    public Container[] containers; // Array of containers to check
    public Text completionText;   // UI text to display completion message

    private int rocksInBins; // Number of rocks currently in bins
    private int totalRocks = 8; // Total number of rocks required for completion

    void Start()
    {
        completionText.text = "Sorting Task:";
        rocksInBins = 0; // Initialize to 0
    }

    public void CheckCompletion()
    {
        bool allRocksSorted = true; // Assume all rocks are sorted initially
        // Debug.Log("Checking Completion");

        // Check each container to see if the correct rock is placed
        foreach (Container container in containers)
        {
            // If any container does not have the correct rock, set allRocksSorted to false
            if (!container.CheckRock())
            {
                allRocksSorted = false;
                break; // No need to check further
            }
        }

        // If all rocks are sorted correctly and all rocks are in bins, display the completion message
        if (allRocksSorted && rocksInBins == totalRocks)
        {
            completionText.text = "Done!";
            // Debug.Log("Completed task");
        }
        else
        {
            completionText.text = "Sorting Task: " + rocksInBins + "/" + totalRocks;
            // Debug.Log("Task incomplete: " + rocksInBins + "/" + totalRocks);
        }
    }

    // Increase the number of rocks in bins
    public void IncreaseRockInBin()
    {
        rocksInBins++; 
        // Debug.Log("Current rocks = " + rocksInBins);
    }

    // Decrease the number of rocks in bins
    public void DecreaseRockInBin()
    {
        if (rocksInBins > 0) // Ensure rocksInBins doesn't go below 0
        {
            rocksInBins--;
        }
        // Debug.Log("Current rocks = " + rocksInBins);
    }
}