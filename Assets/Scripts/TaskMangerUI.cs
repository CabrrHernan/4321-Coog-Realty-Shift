// TaskSelectorUI.cs
using UnityEngine;
using UnityEngine.UI;

public class TaskSelectorUI : MonoBehaviour
{
    public TaskManager taskManager;
    public Button taskButtonPrefab;
    public Transform taskListParent;

    private void Start()
    {
        for (int i = 0; i < taskManager.tasks.Count; i++)
        {
            Button taskButton = Instantiate(taskButtonPrefab, taskListParent);
            taskButton.GetComponentInChildren<Text>().text = taskManager.tasks[i].taskName;
            int taskIndex = i;  // Capture the current index for the button
            taskButton.onClick.AddListener(() => taskManager.ChangeTask(taskIndex));
        }
    }
}
