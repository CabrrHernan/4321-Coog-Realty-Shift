// TaskManager.cs
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public Task currentTask;

    // UI references (Optional)
    public UnityEngine.UI.Text taskDescriptionText;

    private void Start()
    {
        // Example: Initialize some tasks
        tasks.Add(new Task("Put Out Fire", "You need to put out a fire using the fire extinguisher."));
        tasks.Add(new Task("Handle Chemical Spill", "Clean up the chemical spill using proper procedures."));

        // Automatically set the first task
        currentTask = tasks[0];
        DisplayTaskDescription();
    }

    // Function to display the task's description in the UI
    private void DisplayTaskDescription()
    {
        if (taskDescriptionText != null)
        {
            taskDescriptionText.text = currentTask.description;
        }
    }

    // Function to change the current task based on player selection (e.g., from UI)
    public void ChangeTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < tasks.Count)
        {
            currentTask = tasks[taskIndex];
            DisplayTaskDescription();
        }
    }

    // Function to mark the current task as completed
    public void CompleteTask()
    {
        currentTask.MarkCompleted();
        Debug.Log(currentTask.taskName + " completed!");

        // Optional: Trigger robot's "light-up" behavior (You could change light intensity here)
        // lightObject.intensity = 10f; // Example: Turn on a light to indicate task completion
    }
}

