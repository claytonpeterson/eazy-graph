using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphData : ScriptableObject
{
    [SerializeField]
    private List<NodeData> nodes = new List<NodeData>();

    [SerializeField]
    private List<ConnectionData> connections = new List<ConnectionData>();

    public List<NodeData> Nodes => nodes;

    public List<ConnectionData> Connections => connections;

    public static GraphData Create(string name)
    {
        GraphData graph = CreateInstance<GraphData>();

        string path = string.Format("Assets/Resources/{0}.asset", name);
        AssetDatabase.CreateAsset(graph, path);

        return graph;
    }

    public void AddNode(NodeData node)
    {
        nodes.Add(node);

        AddSubAsset(node);
    }

    public void AddConnection(ConnectionData connection)
    {
        connections.Add(connection);

        AddSubAsset(connection);
    }

    public List<NodeData> GetConnections(NodeData node)
    {
        var c = new List<NodeData>();

        for(int i = 0; i < connections.Count; i++)
        {
            var connection = connections[i];
            if(node == connection.a)
            {
                c.Add(connection.b);
            }
        }

        return c;
    }

    public void AddSubAsset(Object asset)
    {
        AssetDatabase.AddObjectToAsset(asset, this);
        AssetDatabase.SaveAssets();
    }

    public void Clear()
    {
        nodes = new List<NodeData>();
        connections = new List<ConnectionData>();
    }
}
