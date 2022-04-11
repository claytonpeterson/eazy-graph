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

    public NodeView CreateNode(Type type, Vector2 position)
    {
        NodeView nodeView = null;
        if (type == typeof(BigTestNode))
        {
            nodeView = new BigTestNode(position);
        }
        else if (type == typeof(LittleTestNode))
        {
            nodeView = new LittleTestNode(position);
        }

        graphView.AddElement(nodeView);
        return nodeView;
    }

    public void AddNodes(Graph graph)
    {
        if (graph == null)
            return;

        foreach (var nodeData in graph.Nodes)
        {
            var nodeView = nodeSpawner.CreateNodeView(
                nodeData.Data,
                nodeData.Position);

            nodeView.guid = nodeData.Guid;

            graphView.AddElement(nodeView);
        }
    }
}
