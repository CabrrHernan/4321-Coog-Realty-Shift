// Task.cs
public class Task
{
    public string taskName;
    public string description;
    public bool isCompleted;

    public Task(string name, string desc)
    {
        taskName = name;
        description = desc;
        isCompleted = false;
    }

    public void MarkCompleted()
    {
        isCompleted = true;
    }
}
