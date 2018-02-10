using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private Queue<Task> TaskQueue = new Queue<Task>();
    private object _queueLock = new object();

    public static WorldManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lock (_queueLock)
        {
            if (TaskQueue.Count > 0)
                TaskQueue.Dequeue();
        }
    }

    public void ScheduleTask(Task newTask)
    {
        lock (_queueLock)
        {
            if (TaskQueue.Count < 100)
                TaskQueue.Enqueue(newTask);
        }
    }
}