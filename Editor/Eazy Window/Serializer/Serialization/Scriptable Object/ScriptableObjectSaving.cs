using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScriptableObjectGraphSaving : ISaveGraph
{
    public void Save(string path, List<NodeView> nodes, List<Edge> edges)
    {
        var graphData = GetGraphData(path);

        // Must create it first!
        AssetDatabase.CreateAsset(graphData, path);
        AssetDatabase.SaveAssets();

        AddNodesToGraph(
            graph: graphData, 
            nodes: nodes);

        AddConnectionsToGraph(
            graphData: graphData, 
            connectedPorts: edges.Where(x => x.input.node != null).ToArray());
    }

    private bool ContainsGraphData(string path)
    {
        return Resources.Load<GraphData>(path) != null;
    }

    private GraphData GetGraphData(string path)
    {
        if(ContainsGraphData(path))
        {
            return LoadGraphData(path);
        }

        return CreateDataGraphInstance(path);
    }

    private GraphData LoadGraphData(string loadPath)
    {
        return Resources.Load<GraphData>(loadPath);
    }

    private GraphData CreateDataGraphInstance(string savePath)
    {
        return ScriptableObject.CreateInstance<GraphData>();
    }

    private void AddNodesToGraph(GraphData graph, List<NodeView> nodes)
    {
        graph.Clear();

        for(int i = 0; i < nodes.Count; i++)
        {
            var pos = nodes[i].GetPosition();

            var nodeData = CreateNodeDataScriptableObject("node");
            nodeData.Type = nodes[i].testNode.GetType();
            nodeData.GUID = nodes[i].guid;
            nodeData.Position = new Vector2(pos.x, pos.y);

            graph.AddNode(nodeData);
        }
    }

    private void AddConnectionsToGraph(GraphData graphData, Edge[] connectedPorts)
    {
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as NodeView;
            var inputNode = connectedPorts[i].input.node as NodeView;

            graphData.AddConnection(
                connection: CreateConnectionScriptableObject(
                    name: "connection", 
                    nodeAGUID: outputNode.guid, 
                    nodeBGUID: inputNode.guid));
        }
    }

    private NodeData CreateNodeDataScriptableObject(string name)
    {
        return CreateNamedScriptableObject<NodeData>(name);
    }

    private ConnectionData CreateConnectionScriptableObject(string name, string nodeAGUID, string nodeBGUID)
    {
        var connection = CreateNamedScriptableObject<ConnectionData>(name);
        connection.nodeAGUID = nodeAGUID;
        connection.nodeBGUID = nodeBGUID;
        return connection;
    }

    private T CreateNamedScriptableObject<T>(string name) where T : ScriptableObject
    {
        var obj = ScriptableObject.CreateInstance<T>();
        obj.name = name;
        return obj;
    }
}
