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
            container: graphData, 
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

            var nodeData = Create<NodeData>("node");
            nodeData.Type = nodes[i].testNode.GetType();

           /* nodeData.name = graphNode.type.ToString();
            nodeData.Type = graphNode.type;*/

            nodeData.Position = new Vector2(pos.x, pos.y);

            graph.AddNode(nodeData);

            //graph.Nodes[i].Position = new Vector2(pos.x, pos.y);
        }

        /*foreach (var graphNode in nodes)
        {
            //Debug.Log("saving: " + graphNode);
            

            //var node = Create<TestNode>("test");
            //container.AddNode(node);

            *//*var node = new NodeData(
                guid: graphNode.guid,
                text: graphNode.text,
                position: graphNode.GetPosition().position);

            if (graphNode.isRoot)
            {
                container.SetRoot(node);
            }

            container.nodes.Add(node);*//*
        }*/
    }

    private void AddConnectionsToGraph(GraphData container, Edge[] connectedPorts)
    {
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as NodeView;
            var inputNode = connectedPorts[i].input.node as NodeView;
            var connection = ScriptableObject.CreateInstance<ConnectionData>();

            /*var connection = new NodeConnection(
                portName: connectedPorts[i].output.portName,
                startNodeGUID: outputNode.guid,
                endNodeGUID: inputNode.guid);
*/

            container.Connections.Add(connection);
        }
    }

    private static T Create<T>(string name) where T : NodeData
    {
        T node = ScriptableObject.CreateInstance<T>();
        node.name = name;
        return node;
    }
}
