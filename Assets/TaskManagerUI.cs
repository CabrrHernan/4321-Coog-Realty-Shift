using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TaskManagerUI : MonoBehaviour
{
    public TextMeshProUGUI taskNameText,taskNameSmallText;
    public TextMeshProUGUI taskDescriptionText;
    public Button nextButton, prevButton, startButton, resetButton, backButton;
    public ScrollRect scrollRect;

    private int currentTaskIndex = 0;

    private void Start()
    {
        UpdateTaskUI();
        taskNameSmallText.gameObject.SetActive(false);

        nextButton.onClick.AddListener(() => ChangeTask(1));
        prevButton.onClick.AddListener(() => ChangeTask(-1));
        startButton.onClick.AddListener(StartSelectedTask);
        resetButton.onClick.AddListener(ResetSelectedTask);
        backButton.onClick.AddListener(BackToSelection);
    }

    private void Update()
    {
        HandleScrollInput();
    }

    private void HandleScrollInput()
    {
        if (scrollRect == null) return;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // Get scroll wheel input
        if (scrollInput != 0)
        {
            scrollRect.verticalNormalizedPosition += scrollInput * 0.1f; // Adjust scrolling speed
        }
    }

    private void ChangeTask(int direction)
    {
        if (TaskManager.Instance.tasks.Count == 0) return;

        currentTaskIndex += direction;
        if (currentTaskIndex < 0) currentTaskIndex = TaskManager.Instance.tasks.Count - 1;
        if (currentTaskIndex >= TaskManager.Instance.tasks.Count) currentTaskIndex = 0;

        UpdateTaskUI();
    }

    private void UpdateTaskUI()
{
    if (TaskManager.Instance.tasks.Count == 0) return;
    
    Task currentTask = TaskManager.Instance.tasks[currentTaskIndex];
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
    {   taskNameSmallText.gameObject.SetActive(true);
        Task selectedTask = TaskManager.Instance.tasks[currentTaskIndex];
        
        selectedTask.StartTask();
        resetButton.gameObject.SetActive(true);

        // Hide unnecessary buttons
        taskNameText.gameObject.SetActive(false);
        
        startButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        scrollRect.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    private void ResetSelectedTask()
    {
        Task selectedTask = TaskManager.Instance.tasks[currentTaskIndex];
        selectedTask.isCompleted = false;
        Debug.Log(selectedTask.taskName + " has been reset.");
        
    }

    private void BackToSelection()
    {
        startButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        taskNameText.gameObject.SetActive(false);
        taskNameSmallText.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        scrollRect.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
    }
}
