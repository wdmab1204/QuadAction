using UnityEngine;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour
{
    public Transform seeker, target;
    GameGrid grid;
    PriorityQueue<Node> priorityQueue;
    HashSet<Node> closedSet;

    void Awake()
    {
        grid = GetComponent<GameGrid>();
    }

    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Node startNode = grid.GetNodeFromWorldPoint(request.startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(request.targetPos);

        if (priorityQueue == null) priorityQueue = new PriorityQueue<Node>(grid.MaxSize);
        else priorityQueue.Clear();

        if (closedSet == null) closedSet = new HashSet<Node>();
        else closedSet.Clear();


        bool success = false;
        priorityQueue.Enqueue(startNode);

        while (priorityQueue.Count > 0)
        {
            Node node = priorityQueue.Dequeue();
            closedSet.Add(node);

            if (node == targetNode)
            {
                success = true;
                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour) + neighbour.weight;
                if (newCostToNeighbour < neighbour.gCost || !priorityQueue.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parentNode = node;

                    if (!priorityQueue.Contains(neighbour))
                        priorityQueue.Enqueue(neighbour);
                }
            }
        }

        Vector3[] path = new Vector3[0]; 
        if (success)
            path = RetracePath(startNode, targetNode);

        callback(new PathResult(success, path, request.callback));
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        Vector2 prevDirection = Vector2.zero;
        List<Vector3> simplifyPath = new List<Vector3>();

        for (int i = 1; i < path.Count; i++)
        {
            var currentDirection = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (prevDirection != currentDirection)
            {
                simplifyPath.Add(path[i].worldPos);
            }
            prevDirection = currentDirection;
        }

        return simplifyPath.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}