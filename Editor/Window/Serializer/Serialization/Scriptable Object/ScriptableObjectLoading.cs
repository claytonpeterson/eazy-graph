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
            var connection = new SaveConnection
            {
                GuidA = edge.nodeAGUID,
                GuidB = edge.nodeBGUID,
                PortAName = edge.nodeAPortName,
                PortBName = edge.nodeBPortName
            };

            graph.Connections.Add(connection);
        }
    }
}
