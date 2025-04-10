using UnityEngine;

[CreateAssetMenu(fileName = "NewTask", menuName = "Task System/Task")]
public class Task : ScriptableObject
{
    public string taskName;
    public bool isCompleted;
    
    public string description;


    public virtual void StartTask()
    {
        Debug.Log("Starting Task: " + taskName);
    }

    public virtual void CompleteTask()
    {
        isCompleted = true;
        Debug.Log(taskName + " completed!");
    }
}
