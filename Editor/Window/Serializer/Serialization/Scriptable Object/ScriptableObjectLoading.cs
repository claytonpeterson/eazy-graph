using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    public Graph Load(GraphData graphData)
    {
        var graph = new Graph();

        LoadNodes(graphData.Nodes, graph);
        LoadEdges(graphData, graph);

        return graph;
    }

    private void LoadNodes(List<NodeData> nodes, Graph graph)
    {
        foreach (var node in nodes)
        {
            var saveNode = ScriptableObject.CreateInstance<NodeData>();
            saveNode.name = node.name;
            saveNode.GUID = node.GUID;
            saveNode.Position = node.Position;
            saveNode.NodeType = node.NodeType;
            saveNode.Data = node.Data;

            graph.Nodes.Add(saveNode);
        }
    }

    private void LoadEdges(GraphData data, Graph graph)
    {
        foreach (var edge in data.Connections)
        {
            var saveConnection = ScriptableObject.CreateInstance<ConnectionData>();
            saveConnection.nodeAGUID = edge.nodeAGUID;
            saveConnection.nodeBGUID = edge.nodeBGUID;
            saveConnection.nodeAPortName = edge.nodeAPortName;
            saveConnection.nodeBPortName = edge.nodeBPortName;

            graph.Connections.Add(saveConnection);
        }
    }
}
