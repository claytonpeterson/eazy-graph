using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectLoading : ILoadGraph
{
    public virtual GraphData Load(string path)
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
            var saveNode = ScriptableObject.CreateInstance<NodeData>();
            saveNode.name = node.name;
            saveNode.GUID = node.GUID;
            saveNode.Position = node.Position;
            saveNode.NodeType = node.NodeType;
            saveNode.Data = node.Data;

            graph.Nodes.Add(node);
        }
    }

    private void LoadEdges(GraphData data, GraphData graph)
    {
        foreach (var edge in data.Connections)
        {
            var saveConnection = ScriptableObject.CreateInstance<ConnectionData>();
            saveConnection.nodeAGUID = edge.nodeAGUID;
            saveConnection.nodeBGUID = edge.nodeBGUID;
            saveConnection.nodeAPortName = edge.nodeAPortName;
            saveConnection.nodeBPortName = edge.nodeBPortName;

            graph.Connections.Add(edge);
        }
    }
}
