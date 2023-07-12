using System;
using System.Collections.Generic;

public interface IGraph<V>
{
    bool AddVertex(V vertex);

    bool ContainsVertex(V vertex);

    IEnumerable<V> Neighbours(V vertex);

    IEnumerable<V> Vertices { get; }

    bool AddTwoEdge(V from, V to, double weight);
}
