using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TaskManagerUI : MonoBehaviour
{
    public TaskManager taskManager; // Reference to TaskManager
    public TextMeshProUGUI taskNameText, taskNameSmallText;
    public TextMeshProUGUI taskDescriptionText;
  

    public Button nextButton, prevButton, startButton, resetButton, backButton;
    public ScrollRect scrollRect;

    private int currentTaskIndex = 0;

    private void Start()
    {
        if (taskManager == null) taskManager = FindObjectOfType<TaskManager>();  // Find TaskManager if not assigned
        UpdateTaskUI();
        taskNameSmallText.gameObject.SetActive(false);
        

        nextButton.onClick.AddListener(() => ChangeTask(1));
        prevButton.onClick.AddListener(() => ChangeTask(-1));
        startButton.onClick.AddListener(StartSelectedTask);
        resetButton.onClick.AddListener(ResetSelectedTask);
        backButton.onClick.AddListener(BackToSelection);
    }

    private void ChangeTask(int direction)
    {
        if (taskManager.runtimeTasks.Count == 0) return;

        currentTaskIndex += direction;
        if (currentTaskIndex < 0) currentTaskIndex = taskManager.runtimeTasks.Count - 1;
        if (currentTaskIndex >= taskManager.runtimeTasks.Count) currentTaskIndex = 0;

        UpdateTaskUI();
    }

    private void UpdateTaskUI()
    {
        if (taskManager.runtimeTasks.Count == 0) return;

        RuntimeTask currentRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
        Task currentTask = currentRuntimeTask.taskAsset;
        taskNameText.text = currentTask.taskName;
        taskNameSmallText.text = currentTask.taskName;
        taskDescriptionText.text = currentTask.description;

        // Reset Scroll Position when switching tasks
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    private void StartSelectedTask()
{
    RuntimeTask selectedRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
    Task selectedTask = selectedRuntimeTask.taskAsset;

    // Reset task state before starting again
    selectedTask.isCompleted = false;

    taskManager.StartTask(currentTaskIndex);

    taskNameSmallText.text = selectedTask.taskName;
    taskDescriptionText.text = selectedTask.description;

    taskNameSmallText.gameObject.SetActive(true);
    taskDescriptionText.gameObject.SetActive(true);
    scrollRect.gameObject.SetActive(true);
    resetButton.gameObject.SetActive(true);
    backButton.gameObject.SetActive(true);

    // Hide main UI
    taskNameText.gameObject.SetActive(false);
    startButton.gameObject.SetActive(false);
    nextButton.gameObject.SetActive(false);
    prevButton.gameObject.SetActive(false);
}


    private void ResetSelectedTask()
    {
        RuntimeTask selectedRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
        Task selectedTask = selectedRuntimeTask.taskAsset;
        selectedTask.isCompleted = false;
        Debug.Log(selectedTask.taskName + " has been reset.");
    }

    private void BackToSelection()
{
    taskManager.ResetTask(currentTaskIndex); 

    taskNameText.gameObject.SetActive(true);
    startButton.gameObject.SetActive(true);
    nextButton.gameObject.SetActive(true);
    prevButton.gameObject.SetActive(true);

    taskNameSmallText.gameObject.SetActive(false);
    taskDescriptionText.gameObject.SetActive(false);
    scrollRect.gameObject.SetActive(false);
    resetButton.gameObject.SetActive(false);
    backButton.gameObject.SetActive(false);
}
}
