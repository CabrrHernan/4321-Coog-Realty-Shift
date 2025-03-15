// TaskCompletion.cs
using UnityEngine;

public class TaskCompletion : MonoBehaviour
{
    public TaskManager taskManager;
    public GameObject robotLight;  // Reference to the robot's light object

    public void OnTaskCompleted()
    {
        taskManager.CompleteTask();
        robotLight.SetActive(true);  // Light up the robot
    }
}
