using System;
using UnityEngine;

public class NodeCreator
{
    private EditorGraphView graphView;
    private readonly INodeSpawner nodeSpawner;

    public NodeCreator(EditorGraphView graphView, INodeSpawner nodeSpawner)
    {
        this.graphView = graphView;
        this.nodeSpawner = nodeSpawner;
    }

    public NodeView CreateNode(Type type, Vector2 position, TestingOutData data)
    {
        var nodeView = nodeSpawner.CreateNodeView(type, position, data);
        graphView.AddElement(nodeView);
        return nodeView;
    }

    public void AddNodes(GraphData graph)
    {
        if (graph == null)
            return;

        foreach (var nodeData in graph.Nodes)
        {
            var nodeView = nodeSpawner.CreateNodeView(
                Type.GetType(nodeData.NodeType),
                nodeData.Position,
                nodeData.Data);

            nodeView.guid = nodeData.GUID;

            graphView.AddElement(nodeView);
        }
    }
}
