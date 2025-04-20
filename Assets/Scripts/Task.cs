using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Task")]
public class Task : ScriptableObject
{
    public string taskName;
    public string description;
    public bool isCompleted;

    public void StartTask()
    {
        // Logic to start the task
        Debug.Log($"{taskName} has started.");
    }

    public void CompleteTask()
    {
        // Logic to complete the task
        isCompleted = true;
        Debug.Log($"{taskName} has been completed.");
    }

    public void ResetTask()
    {
        // Logic to reset the task
        isCompleted = false;
        Debug.Log($"{taskName} has been reset.");
    }
}
