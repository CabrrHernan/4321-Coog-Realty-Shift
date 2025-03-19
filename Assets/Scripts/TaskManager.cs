using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    public List<Task> tasks = new List<Task>(); // Drag & Drop tasks in Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartNextTask();
    }

    public void TaskCompleted(Task completedTask)
    {
        Debug.Log("Task Completed: " + completedTask.taskName);
        StartNextTask();
    }

    private void StartNextTask()
    {
        foreach (Task task in tasks)
        {
            if (!task.isCompleted)
            {
                task.StartTask();
                return;
            }
        }
        Debug.Log("All tasks completed!");
    }
}
