using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    [SerializeField]
    private List<NodeData> nodes = new List<NodeData>();

    [SerializeField]
    private List<SaveConnection> connections = new List<SaveConnection>();

    public List<NodeData> Nodes 
    { 
        get => nodes; 
    }

    public List<SaveConnection> Connections 
    { 
        get => connections; 
    }

    public void AddNode(NodeData node)
    {
        nodes.Add(node);
    }

    public void AddConnection(SaveConnection connection)
    {
        connections.Add(connection);
    }

    public List<NodeData> Inputs(string guid)
    {
        var inputs = new List<NodeData>();

        foreach (var connection in connections)
        {
            if (connection.GuidB == guid)
            {
                inputs.Add(GetNode(connection.GuidA));
            }
        }
        return inputs;
    }

    public List<NodeData> Outputs(string guid)
    {
        var outputs = new List<NodeData>();

        foreach (var connection in connections)
        {
            if (connection.GuidA == guid)
            {
                outputs.Add(GetNode(connection.GuidB));
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
