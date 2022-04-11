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
}
