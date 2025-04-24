using UnityEngine;

public class Container : MonoBehaviour
{
    public string expectedRockType; 
    public string expectedBin;  

    private Rock placedRock; 
    private sortingTaskManager taskManager; 

    private void Start()
    {
        taskManager = FindFirstObjectByType<sortingTaskManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null)
        {
            placedRock = rock; 
            // Debug.Log("Rock placed in container: " + rock.rockType);

            if (taskManager != null)
            {
                taskManager.IncreaseRockInBin();
                taskManager.CheckCompletion(); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null && rock == placedRock)
        {
            placedRock = null; // Clear the reference when the rock is removed
            // Debug.Log("Rock removed from container: " + rock.rockType);

            if (taskManager != null)
            {
                taskManager.DecreaseRockInBin();
                taskManager.CheckCompletion();
            }
        }
    }
    public bool CheckRock()
    {
        if (placedRock == null)
        {
            return false;
        }

        return placedRock.rockType == expectedRockType && placedRock.GetCorrectBin() == expectedBin;
    }
}