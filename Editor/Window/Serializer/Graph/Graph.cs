using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    [SerializeField]
    private List<NodeData> nodes = new List<NodeData>();

    [SerializeField]
    private List<ConnectionData> connections = new List<ConnectionData>();

    public List<NodeData> Nodes 
    { 
        get => nodes; 
    }

    public List<ConnectionData> Connections 
    { 
        get => connections; 
    }

    public void AddNode(NodeData node)
    {
        nodes.Add(node);
    }

    public void AddConnection(ConnectionData connection)
    {
        connections.Add(connection);
    }

    public List<NodeData> Inputs(string guid)
    {
        var inputs = new List<NodeData>();

        foreach (var connection in connections)
        {
            if (connection.nodeBGUID == guid)
            {
                inputs.Add(GetNode(connection.nodeAGUID));
            }
        }
        return inputs;
    }

    public List<NodeData> Outputs(string guid)
    {
        var outputs = new List<NodeData>();

        foreach (var connection in connections)
        {
            if (connection.nodeAGUID == guid)
            {
                outputs.Add(GetNode(connection.nodeBGUID));
            }
        }
        return outputs;
    }

    public NodeData GetNode(string guid)
    {
        foreach(var node in nodes)
        {
            if (node.GUID == guid)
                return node;
        }
        return null;
    }
}
