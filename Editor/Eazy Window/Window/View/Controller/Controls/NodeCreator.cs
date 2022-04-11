using System;
using UnityEditor.Experimental.GraphView;
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
        var portInformation = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Multi
        };

        NodeView nodeView = null;
        if (type == typeof(BigTestNode))
        {
            nodeView = new BigTestNode(position, portInformation);
        }
        else if (type == typeof(LittleTestNode))
        {
            nodeView = new LittleTestNode(position, portInformation);
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
            var portInformation = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Multi
            };

            var nodeView = nodeSpawner.CreateNodeView(
                nodeData.Data,
                nodeData.Position,
                portInformation);

            nodeView.guid = nodeData.Guid;

            graphView.AddElement(nodeView);
        }
    }
}
