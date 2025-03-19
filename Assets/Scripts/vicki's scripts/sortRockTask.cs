using UnityEngine;
using UnityEngine.UI;

public class sortingTaskManager : MonoBehaviour
{
    public Container[] containers; // Array of containers to check
    public Text completionText;   // UI text to display completion message

    void Start()
    {
        // Set custom streak colors for rocks
        Rock.SetStreakColor("hematite", Rock.Brown); // Use custom brown color
        Rock.SetStreakColor("magnatite", Rock.LightBlack); // Use custom dark brown color


    }

    public void CheckCompletion()
    {
        bool allRocksSorted = true;

        // Check each container to see if the correct rock is placed
        foreach (Container container in containers)
        {
            // Get the Rock component from the object placed in the container
            Rock placedRock = container.GetComponentInChildren<Rock>();

            if (placedRock == null || !container.CheckRock(placedRock))
            {
                allRocksSorted = false;
                break;
            }
        }

        // If all rocks are sorted correctly, display the completion message
        if (allRocksSorted)
        {
            completionText.text = "Task Complete!";
            Debug.Log("Task Complete!");
        }
    }
}