using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public struct PathRequest
{
    public Vector3 startPos;
    public Vector3 targetPos;
    public Action<bool, Vector3[]> callback;

    public PathRequest(Vector3 startPos, Vector3 targetPos, Action<bool, Vector3[]> callback)
    {
        this.startPos = startPos;
        this.targetPos = targetPos;
        this.callback = callback;
    }
}

public struct PathResult
{
    public bool success;
    public Vector3[] path;
    public Action<bool, Vector3[]> callback;

    public PathResult(bool success, Vector3[] path, Action<bool, Vector3[]> callback)
    {
        this.success = success;
        this.path = path;
        this.callback = callback;
    }
}


public class PathRequestManager : MonoBehaviour
{
    static PathRequestManager instance;
    PathFinding pathFinding;
    Queue<PathResult> resultQueue = new Queue<PathResult>();
    bool isProcessing = false;
    PathRequest currentRequest;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    private void Update()
    {
        if (resultQueue.Count > 0)
        {
            lock (resultQueue)
            {
                int itemCount = resultQueue.Count;
                while (itemCount-- > 0)
                {
                    PathResult result = resultQueue.Dequeue();
                    result.callback(result.success, result.path);
                }
            }
        }
    }

    public static void RequestPathFind(PathRequest request)
    {
        ThreadStart threadStart = delegate
        {
            instance.pathFinding.FindPath(request, instance.FinishProcessingFindPath);
        };

        threadStart.Invoke();
    }

    public void FinishProcessingFindPath(PathResult result)
    {
        lock (resultQueue)
        {
            resultQueue.Enqueue(result);
        }
    }
}
