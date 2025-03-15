// ResetTask.cs
using UnityEngine;

public class ResetTask : MonoBehaviour
{
    public TaskManager taskManager;
    public GameObject robotLight;  // Reference to the robot's light object

    public void ResetCurrentTask()
    {
        taskManager.currentTask.isCompleted = false;
        robotLight.SetActive(false);  // Turn off the robot light when resetting
    }
}
