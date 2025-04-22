using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public List<RuntimeTask> runtimeTasks = new List<RuntimeTask>(); // Assign in Inspector

    private void Awake()
{
    foreach (var task in runtimeTasks)
    {
        task.CacheOriginalTransform();
    }
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
    {
        rt.ResetTransform(); // Now resets all children too
        rt.taskObject.SetActive(false);
        rt.taskObject.SetActive(true); // Optional: restart behavior
    }
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
    public Task taskAsset;
    public GameObject taskObject;

    [HideInInspector] public List<TransformData> originalTransforms = new List<TransformData>();

    public void CacheOriginalTransform()
    {
        originalTransforms.Clear();

        if (taskObject == null) return;

        foreach (Transform t in taskObject.GetComponentsInChildren<Transform>(true))
        {
            originalTransforms.Add(new TransformData
            {
                transform = t,
                position = t.position,
                rotation = t.rotation,
                scale = t.localScale
            });
        }
    }

    public void ResetTransform()
    {
        foreach (var tData in originalTransforms)
        {
            if (tData.transform != null)
            {
                tData.transform.position = tData.position;
                tData.transform.rotation = tData.rotation;
                tData.transform.localScale = tData.scale;
            }
        }
    }
}

[System.Serializable]
public class TransformData
{
    public Transform transform;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

