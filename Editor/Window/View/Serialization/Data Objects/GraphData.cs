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

        /*for(int i = 0; i < connections.Count; i++)
        {
            var connection = connections[i];
            if(node.GUID == connection.nodeAGUID)
            {
                c.Add(connection.nodeBGUID);
            }
        }
*/
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
        foreach (var node in nodes)
        {
            if (node.GUID == guid)
                return node;
        }
        return null;
    }
}
