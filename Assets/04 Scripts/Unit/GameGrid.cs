using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    /// <summary>
    /// Distance from starting node
    /// </summary>
    public int gCost;

    /// <summary>
    /// Distance from end node
    /// </summary>
    public int hCost;

    /// <summary>
    /// F cost = G + H
    /// </summary>
    public int fCost { get => gCost + hCost; }

    public Node parentNode;

    public bool isWalkable;

    public Vector3 worldPos;
    public int gridX;
    public int gridY;

    public Node(bool isWalkable, Vector3 worldPos, int gridX, int gridY)
    {
        this.isWalkable = isWalkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    int IComparable<Node>.CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(other.hCost);

        return compare;
    }
}

public class GameGrid : MonoBehaviour
{
    public float nodeRadius = 1;
    public Vector2 gridWorldSize;
    public LayerMask unwalkableMask;
    public bool showGridGizmos;

    public int MaxSize { get => gridSizeX * gridSizeY; }

    int gridSizeX, gridSizeY;
    float nodeDiameter;

    Node[,] grid;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        Vector3 worldPoint;

        for(int x=0; x<gridSizeX; x++)
        {
            for(int y=0; y<gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x=-1; x<=1; x++)
        {
            for(int y=-1; y<=1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && showGridGizmos)
        {
            foreach(var node in grid)
            {
                Gizmos.color = node.isWalkable ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(node)) Gizmos.color = Color.black;
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - .2f));
            }
        }
    }
}
