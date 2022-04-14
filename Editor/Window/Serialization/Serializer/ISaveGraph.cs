using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public interface ISaveGraph
{
    void Save(string path, List<NodeView> nodes, List<Edge> edges);
}
