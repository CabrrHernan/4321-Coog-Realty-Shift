using UnityEngine;
using UnityEngine.UI;

public class sortingTaskManager : MonoBehaviour
{
    public Container[] containers; 
    public Text completionText; 

    private int rocksInBins; 
    private int totalRocks = 8; 

    void Start()
    {
        completionText.text = "Sorting Task:";
        rocksInBins = 0;
    }

    public void CheckCompletion()
    {
        bool allRocksSorted = true; 
        // Debug.Log("Checking Completion");

        foreach (Container container in containers)
        {
            if (!container.CheckRock())
            {
                allRocksSorted = false;
                break; 
            }
        }
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

    public void IncreaseRockInBin()
    {
        rocksInBins++; 
        // Debug.Log("Current rocks = " + rocksInBins);
    }

    public void DecreaseRockInBin()
    {
        if (rocksInBins > 0) // Make sure stays positive will break otherwise
        {
            rocksInBins--;
        }
        // Debug.Log("Current rocks = " + rocksInBins);
    }
}