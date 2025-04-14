using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public List<RuntimeTask> runtimeTasks = new List<RuntimeTask>(); // Assign in Inspector

    private void Awake()
    {
        // Optionally initialize something if needed
    }

    public void StartTask(int index)
{
    if (index < 0 || index >= runtimeTasks.Count) return;

    var rt = runtimeTasks[index];
    rt.taskAsset.isCompleted = false;

    // Check if taskObject is not null before calling SetActive
    if (rt.taskObject != null)
    {
        rt.taskObject.SetActive(true);
    }

    rt.taskAsset.StartTask();
}


    public void ResetTask(int index)
    {
        if (index < 0 || index >= runtimeTasks.Count) return;

        var rt = runtimeTasks[index];
        rt.taskAsset.isCompleted = false;

        if (rt.taskObject != null)
            rt.taskObject.SetActive(false);
    }

    public void CompleteTask(int index)
    {
        if (index < 0 || index >= runtimeTasks.Count) return;

        var rt = runtimeTasks[index];
        rt.taskAsset.CompleteTask();

        if (rt.taskObject != null)
            rt.taskObject.SetActive(false);
    }
}

[System.Serializable]
public class RuntimeTask
{
    public Task taskAsset; // Reference to the Task ScriptableObject
    public GameObject taskObject; // The corresponding GameObject for this task
}
