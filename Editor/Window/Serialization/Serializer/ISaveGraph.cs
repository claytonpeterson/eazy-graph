using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace skybirdgames.eazygraph.Editor
{
    public interface ISaveGraph
    {
        GraphData Save(string path, List<NodeView> nodes, List<Edge> edges);
    }
}