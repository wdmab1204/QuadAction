using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour
{
    public Transform seeker, target;
    GameGrid grid;

    void Awake()
    {
        grid = GetComponent<GameGrid>();
    }

    void Update()
    {
        //PathRequestManager.RequestPathFind(seeker.position, target.position, OnPathFind);
        //FindPath(seeker.position, target.position);
    }

    void OnPathFind()
    {
        //follow target
    }

    public void FindPath(Vector3 startPos, Vector3 targetPos, Action<bool, Vector3[]> callback)
    {
        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        bool success = false;
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
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

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parentNode = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        callback(success, RetracePath(startNode, targetNode));
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        List<Vector3> wayPoints = new List<Vector3>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            wayPoints.Add(currentNode.worldPos);
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        wayPoints.Reverse();
        path.Reverse();

        grid.path = path;

        return wayPoints.ToArray();

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