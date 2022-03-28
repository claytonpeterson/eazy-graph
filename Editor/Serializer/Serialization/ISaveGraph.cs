using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public interface ISaveGraph
{
    void Save(string path, List<GraphNode> nodes, List<Edge> edges);
}
