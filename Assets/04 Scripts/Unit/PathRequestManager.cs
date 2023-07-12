using System;
using System.Collections.Generic;
using UnityEngine;

struct PathRequest
{
    public Vector3 startPos;
    public Vector3 targetPos;
    public Action callback;

    public PathRequest(Vector3 startPos, Vector3 targetPos, Action callback)
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

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPathFind(Vector3 startPos, Vector3 targetPos, Action callback)
    {
        PathRequest pathRequest = new PathRequest(startPos, targetPos, callback);
        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessing &&instance.pathRequestQueue.Count > 0)
        {
            var request = pathRequestQueue.Dequeue();
            isProcessing = true;
            instance.pathFinding.FindPath(request.startPos, request.targetPos, FinishProcessingFindPath);
        }
    }

    void FinishProcessingFindPath(bool success, Vector3[] waypoints)
    {
        isProcessing = false;
        TryProcessNext();
    }
}
