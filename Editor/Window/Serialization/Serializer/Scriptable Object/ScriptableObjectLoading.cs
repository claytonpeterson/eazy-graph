using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectLoading : ILoadGraph
{
    public GraphData Load(string path)
    {
        var graphData = AssetDatabase.LoadAssetAtPath<GraphData>(path);
        var graph = ScriptableObject.CreateInstance<GraphData>();

        LoadNodes(graphData.Nodes, graph);
        LoadEdges(graphData, graph);

        return graph;
    }

    public GraphData Load(GraphData graphData)
    {
        var graph = ScriptableObject.CreateInstance<GraphData>();

        LoadNodes(graphData.Nodes, graph);
        LoadEdges(graphData, graph);

        return graph;
    }

    private void LoadNodes(List<NodeData> nodes, GraphData graph)
    {
        foreach (var node in nodes)
        {
            graph.Nodes.Add(node);
        }
    }

    private void LoadEdges(GraphData data, GraphData graph)
    {
        foreach (var edge in data.Connections)
        {
            graph.Connections.Add(edge);
        }
    }
}
