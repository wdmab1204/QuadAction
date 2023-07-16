using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

struct PathRequest
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


public class PathRequestManager : MonoBehaviour
{
    static PathRequestManager instance;
    PathFinding pathFinding;
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    bool isProcessing = false;
    PathRequest currentRequest;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPathFind(Vector3 startPos, Vector3 targetPos, Action<bool, Vector3[]> callback)
    {
        PathRequest pathRequest = new PathRequest(startPos, targetPos, callback);
        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessNext();

        ThreadStart threadStart = delegate
        {
            instance.pathFinding.FindPath(startPos, targetPos, callback);
        };
    }

    void TryProcessNext()
    {
        if (!isProcessing &&instance.pathRequestQueue.Count > 0)
        {
            currentRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            instance.pathFinding.FindPath(currentRequest.startPos, currentRequest.targetPos, FinishProcessingFindPath);
        }
    }

    void FinishProcessingFindPath(bool success, Vector3[] waypoints)
    {
        isProcessing = false;
        currentRequest.callback(success, waypoints);
        TryProcessNext();
    }
}
