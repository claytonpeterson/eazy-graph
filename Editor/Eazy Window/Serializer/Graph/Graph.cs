using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    [SerializeField]
    private List<SaveNode> nodes = new List<SaveNode>();

    [SerializeField]
    private List<SaveConnection> connections = new List<SaveConnection>();

    public List<SaveNode> Nodes 
    { 
        get => nodes; 
    }

    public List<SaveConnection> Connections 
    { 
        get => connections; 
    }

    public void AddNode(SaveNode node)
    {
        nodes.Add(node);
    }

    public void AddConnection(SaveConnection connection)
    {
        connections.Add(connection);
    }

    public List<SaveNode> Inputs(string guid)
    {
        var inputs = new List<SaveNode>();

        foreach (var connection in connections)
        {
            if (connection.GuidB == guid)
            {
                inputs.Add(GetNode(connection.GuidA));
            }
        }

        return inputs;
    }

    public SaveNode GetNode(string guid)
    {
        foreach(var node in nodes)
        {
            if (node.Guid == guid)
                return node;
        }
        return null;
    }
}
