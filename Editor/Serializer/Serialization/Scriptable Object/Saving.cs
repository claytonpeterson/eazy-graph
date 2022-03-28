using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Saving
{
    public void Save(string fileName, List<NodeView> nodes, List<Edge> edges)
    {
        // the path should be passed, not just the file name!
        if (fileName == null)
            fileName = "New";
        var path = string.Format("Assets/Resources/{0}.asset", fileName);

        var container = GetContainer(path);

        //var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

        SaveNodes(container, nodes);
        //SaveConnections(container, connectedPorts);

        if(!AssetDatabase.Contains(container))
        {
            AssetDatabase.CreateAsset(container, path: path);
        }

        AssetDatabase.SaveAssets();
    }

    private GraphData GetContainer(string path)
    {
        var container = Resources.Load<GraphData>(path);
        
        if(container != null)
        {
            Debug.Log("creating container");
            return container;
        }

        return CreateContainer(path);
    }

    private GraphData CreateContainer(string path)
    {
        var testContainer = ScriptableObject.CreateInstance<GraphData>();

        AssetDatabase.CreateAsset(testContainer, path);
/*
        var spriteNode = Create<SpriteTestNode>("Sprite Node");
        var numberNode = Create<NumberNodeData>("Number Node");

        testContainer.AddNode(spriteNode);
        testContainer.AddNode(numberNode);
*/
        return testContainer;
    }

    private void SaveNodes(GraphData graph, List<NodeView> nodes)
    {
        graph.Clear();

        for(int i = 0; i < nodes.Count; i++)
        {
            var graphNode = nodes[i];
            var pos = nodes[i].GetPosition();

            /*NodeData nodeData;
            if(graphNode.type == typeof(CompositeNode))
            {
                //nodeData = Create<COm>("Composite Node");
            }
            else if (graphNode.type == typeof(LeafNodeView))
            {

            }
            else if (graphNode.type == typeof(DecoratorNode))
            {

            }*/

            var nodeData = Create<NodeData>("node");
            nodeData.name = graphNode.type.ToString();
            nodeData.Type = graphNode.type;
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

    private void SaveConnections(GraphData container, Edge[] connectedPorts)
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

            //container.connections.Add(connection);
        }
    }

    private static T Create<T>(string name) where T : NodeData
    {
        T node = ScriptableObject.CreateInstance<T>();
        node.name = name;
        return node;
    }
}
