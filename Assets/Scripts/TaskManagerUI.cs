using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TaskManagerUI : MonoBehaviour
{
    public TaskManager taskManager; // Reference to TaskManager
    public TextMeshProUGUI taskNameText, taskNameSmallText;
    public TextMeshProUGUI taskDescriptionText;
    public Toggle completeToggle; // Checkbox to mark completion

    public Button nextButton, prevButton, startButton, resetButton, backButton;
    public ScrollRect scrollRect;

    private int currentTaskIndex = 0;

    private void Start()
    {
        if (taskManager == null) taskManager = FindObjectOfType<TaskManager>();  // Auto-assign TaskManager
        UpdateTaskUI();
        taskNameSmallText.gameObject.SetActive(false);
        completeToggle.interactable = false;

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

        completeToggle.isOn = currentTask.isCompleted;
        completeToggle.interactable = false;

        // Reset scroll position
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    private void StartSelectedTask()
    {
        RuntimeTask selectedRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
        Task selectedTask = selectedRuntimeTask.taskAsset;

        selectedTask.isCompleted = false; // Reset task completion on start
        taskManager.StartTask(currentTaskIndex);

        taskNameSmallText.text = selectedTask.taskName;
        taskDescriptionText.text = selectedTask.description;

        taskNameSmallText.gameObject.SetActive(true);
        taskDescriptionText.gameObject.SetActive(true);
        scrollRect.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        taskNameText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);

        completeToggle.interactable = true;
        completeToggle.isOn = selectedTask.isCompleted;
        completeToggle.onValueChanged.RemoveAllListeners(); // Prevent stacking
        completeToggle.onValueChanged.AddListener((isOn) =>
        {
            selectedTask.isCompleted = isOn;
        });
    }

    private void ResetSelectedTask()
{
    taskManager.ResetTask(currentTaskIndex); // ← Actually resets the task object now

    RuntimeTask selectedRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
    Task selectedTask = selectedRuntimeTask.taskAsset;

    selectedTask.isCompleted = false;
    completeToggle.isOn = false;

    // Re-update the UI to reflect any changed state
    taskDescriptionText.text = selectedTask.description;

    Debug.Log($"{selectedTask.taskName} has been reset.");
}


    private void BackToSelection()
{
    // Reset task (reset transform & data)
    taskManager.ResetTask(currentTaskIndex); 

    // Deactivate taskObject manually
    var currentRuntimeTask = taskManager.runtimeTasks[currentTaskIndex];
    if (currentRuntimeTask.taskObject != null)
    {
        currentRuntimeTask.taskObject.SetActive(false);
    }

    // UI: Switch to selection screen
    taskNameText.gameObject.SetActive(true);
    startButton.gameObject.SetActive(true);
    nextButton.gameObject.SetActive(true);
    prevButton.gameObject.SetActive(true);

    taskNameSmallText.gameObject.SetActive(false);
    taskDescriptionText.gameObject.SetActive(false);
    scrollRect.gameObject.SetActive(false);
    resetButton.gameObject.SetActive(false);
    backButton.gameObject.SetActive(false);

    completeToggle.interactable = false;
    completeToggle.onValueChanged.RemoveAllListeners();
}

}
