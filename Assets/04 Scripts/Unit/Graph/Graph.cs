using System;
using System.Collections.Generic;
using System.Text;

public class Graph<T> : IGraph<T>
{
    public class Edge
    {
        public T From { get; }
        public T To { get; }
        public double Weight { get; }

        public Edge(T from, T to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    // Dictionary: { 1 -> {2, 3}, 2 -> {1}, 3 -> {1} }
    public Dictionary<T, List<Edge>> AdjacencyList { get; } = new Dictionary<T, List<Edge>>();

    public Graph() { }

    public Graph(int initialSize)
    {
        AdjacencyList = new Dictionary<T, List<Edge>>();
    }

    public bool AddVertex(T vertex)
    {
        if (ContainsVertex(vertex))
            return false;

        AdjacencyList[vertex] = new List<Edge>();
        return true;
    }

    public bool ContainsVertex(T vertex) => AdjacencyList.ContainsKey(vertex);

    public IEnumerable<T> Neighbours(T vertex) => (IEnumerable<T>)AdjacencyList[vertex];

    public IEnumerable<T> Vertices => AdjacencyList.Keys;

    public bool AddEdge(T from, T to, double weight)
    {
        if (AdjacencyList.ContainsKey(from) && AdjacencyList.ContainsKey(to))
        {
            AdjacencyList[from].Add(new Edge(from, to, weight));
            return true;
        }

        return false;
    }

    public bool AddTwoEdge(T from, T to, double weight)
    {
        if (AdjacencyList.ContainsKey(from) && AdjacencyList.ContainsKey(to))
        {
            AdjacencyList[from].Add(new Edge(from, to, weight));
            AdjacencyList[to].Add(new Edge(to, from, weight));
            return true;
        }

        return false;
    }

    // public IEnumerable< Tuple<T,T> > Edges {get;} // ToDo

}