using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScriptableObjectGraphSaving : ISaveGraph
{
    private struct ConnectionInfo
    {
        public string nodeGuid;
        public string portName;
    }

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
            nodeData.NodeType = nodes[i].GetType().AssemblyQualifiedName;
            nodeData.GUID = nodes[i].guid;
            nodeData.Data = nodes[i].Data();
            nodeData.Position = new Vector2(pos.x, pos.y);

            graph.AddNode(nodeData);
        }
    }

    private void AddConnectionsToGraph(GraphData graphData, Edge[] connectedPorts)
    {
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputPort = connectedPorts[i].output;
            var outputNode = outputPort.node as NodeView;

            var inputPort = connectedPorts[i].input;
            var inputNode = inputPort.node as NodeView;

            graphData.AddConnection(
                connection: CreateConnectionScriptableObject(
                    name: "connection", 
                    nodeA: new ConnectionInfo 
                    { 
                        nodeGuid = outputNode.guid, 
                        portName = outputPort.portName 
                    }, 
                    nodeB: new ConnectionInfo 
                    { 
                        nodeGuid = inputNode.guid, 
                        portName = inputPort.portName 
                    }
                )
            );
        }
    }

    private NodeData CreateNodeDataScriptableObject(string name)
    {
        return CreateNamedScriptableObject<NodeData>(name);
    }

    private ConnectionData CreateConnectionScriptableObject(string name, ConnectionInfo nodeA, ConnectionInfo nodeB)
    {
        var connection = CreateNamedScriptableObject<ConnectionData>(name);
        
        connection.nodeAGUID = nodeA.nodeGuid;
        connection.nodeAPortName = nodeA.portName;

        connection.nodeBGUID = nodeB.nodeGuid;
        connection.nodeBPortName = nodeB.portName;

        return connection;
    }

    private T CreateNamedScriptableObject<T>(string name) where T : ScriptableObject
    {
        var obj = ScriptableObject.CreateInstance<T>();
        obj.name = name;
        return obj;
    }
}
