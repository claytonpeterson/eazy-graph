using System.Collections.Generic;
using UnityEditor;

public class ScriptableObjectLoading : ILoadGraph
{
    public Graph Load(string path)
    {
        var graphData = AssetDatabase.LoadAssetAtPath<GraphData>(path);
        var graph = new Graph();

        LoadNodes(graphData.Nodes, graph);
        LoadEdges(graphData, graph);

        return graph;
    }

    private void LoadNodes(List<NodeData> nodes, Graph graph)
    {
        foreach (var node in nodes)
        {
            var saveNode = new SaveNode
            {
                Name = node.name,
                Guid = node.GUID,
                Position = node.Position,
                Data = node.Type
            };

            graph.Nodes.Add(saveNode);
        }
    }

    private void LoadEdges(GraphData data, Graph graph)
    {
        foreach (var edge in data.Connections)
        {
            var connection = new SaveConnection
            {
                GuidA = edge.nodeAGUID,
                GuidB = edge.nodeBGUID
            };

            graph.Connections.Add(connection);
        }
    }
}
